using Azure.Core;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using ReCharge.Data.Interfaces;
using ReChargeBackend.Requests;
using ReChargeBackend.Responses;
using ReChargeBackend.Utility;
using Utility;

namespace BackendReCharge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReservationsController : ControllerBase
    {
        private Dictionary<int, CancellationTokenSource> canceletionTokenSources = new Dictionary<int, CancellationTokenSource>();
        private IReservationRepository reservationRepository;
        private IUserRepository userRepository;
        private ISlotRepository slotRepository;
        private IAdminUserRepository adminUserRepository;
        private readonly ILogger<ReservationsController> _logger;
        private readonly IAdminUserRepository adminRepository;

        public ReservationsController(ILogger<ReservationsController> logger,
            IReservationRepository reservationRepository,
            IUserRepository userRepository,
            ISlotRepository slotRepository,
            IAdminUserRepository adminUserRepository,
            IAdminUserRepository adminRepository)
        {
            _logger = logger;
            this.reservationRepository = reservationRepository;
            this.userRepository = userRepository;
            this.slotRepository = slotRepository;
            this.adminUserRepository = adminUserRepository;
            this.adminRepository = adminRepository;
        }

        [HttpPost(Name = "MakeReservation")]
        public async Task<IActionResult> MakeReservation(int slotId, [FromBody] MakeReservationRequest request)
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Отсутствует токен доступа");
            }
            var user = await userRepository.GetByAccessTokenAsync(token);
            if (user is null)
            {
                return NotFound("Пользоватен не найден (невалидный токен доступа)");
            }
            var slot = await slotRepository.GetByIdAsync(slotId);
            if (slot is null)
            {
                return BadRequest($"Слот с id {slotId} не найден");
            }
            var usersReses = (await reservationRepository.GetReservationsByUserAsync(user.Id)).ToList();
            foreach (var i in usersReses)
            {
                Console.WriteLine(i.SlotId);
            }
            if (usersReses.Any(x => x.SlotId == slotId && (x.Status == Status.New || x.Status == Status.Confirmed)))
            {
                return StatusCode(452, "Вы уже записаны на это занятие");
            }
            if (slot.SlotDateTime < DateTime.Now)
            {
                return BadRequest("Активность уже прошла");
            }
            if (slot.FreePlaces >= request.ReserveCount)
            {
                var res = await reservationRepository.AddAsync(new Reservation
                {
                    Slot = slot,
                    SlotId = slotId,
                    User = user,
                    UserId = user.Id,
                    Count = request.ReserveCount,
                    Email = request.Email,
                    Name = request.Name,
                    PhoneNumber = request.Phone,
                    AccessCode = Temp.GenerateAccessCode(),
                    Status = Status.New
                });
                slot.FreePlaces -= request.ReserveCount;
                await slotRepository.UpdateAsync(slot);
                    canceletionTokenSources.Add(res.Id, new CancellationTokenSource());
                    _setReservationMissed(res.Id, slot.SlotDateTime.AddMinutes(slot.LengthMinutes), canceletionTokenSources[res.Id].Token);
                AdminUser? admin;
                try
                {
                    admin = (await adminUserRepository.GetAllAsync()).First(x => x.LocationId == slot.Activity.LocationId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    return BadRequest("This location doens't have an admin");
                }
                string hours = slot.SlotDateTime.Hour > 9 ? slot.SlotDateTime.Hour.ToString() : "0" + slot.SlotDateTime.Hour;
                string minutes = slot.SlotDateTime.Minute > 9 ? slot.SlotDateTime.Minute.ToString() : "0" + slot.SlotDateTime.Minute;
                string day = slot.SlotDateTime.Day > 9 ? slot.SlotDateTime.Day.ToString() : "0" + slot.SlotDateTime.Day;
                string month = slot.SlotDateTime.Month > 9 ? slot.SlotDateTime.Month.ToString() : "0" + slot.SlotDateTime.Month;

                if (admin.FirebaseToken != null)
                {
                    NotificationManager.NotifyUser("Бронь требует подтверждения", $"{slot.Activity.ActivityName} в {slot.Activity.Location.LocationName}\n" +
                        $"{day}.{month} в {hours}:{minutes}",
                        slot.Activity.ImageUrl  == null ? "" : slot.Activity.ImageUrl,
                        admin.FirebaseToken, DateTime.Now, new CancellationToken());
                }
                if (user.FirebaseToken != null)
                {
                    NotificationManager.NotifyUser("У вас завтра занятие!", $"{slot.Activity.ActivityName} в {slot.Activity.Location.LocationName}\n" +
                        $"{day}.{month} в {hours}:{minutes}",
                        slot.Activity.ImageUrl ?? "",
                        user.FirebaseToken, new DateTime(
                            slot.SlotDateTime.Year, 
                            slot.SlotDateTime.Month, 
                            slot.SlotDateTime.Day - 1, 
                            18, 30, 0),
                        canceletionTokenSources[res.Id].Token
                        );
                    NotificationManager.NotifyUser("У вас занятие через 2 часа!", $"{slot.Activity.ActivityName} в {slot.Activity.Location.LocationName}\n" +
                        $"{day}.{month} в {hours}:{minutes}",
                        slot.Activity.ImageUrl ?? "",
                        user.FirebaseToken, new DateTime(
                            slot.SlotDateTime.Year,
                            slot.SlotDateTime.Month,
                            slot.SlotDateTime.Day,
                            slot.SlotDateTime.Hour - 2,
                            slot.SlotDateTime.Minute, 0),
                        canceletionTokenSources[res.Id].Token
                        );
                }

                return Ok();
            }
            return BadRequest($"Не достаточно свободных мест на слоте. Достуных мест: {slot.FreePlaces}");

        }
        [HttpGet(Name = "GetNextReservation")]
        public async Task<IActionResult> GetNextReservation()
        {

            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Отсутствует токен доступа");
            }
            var user = await userRepository.GetByAccessTokenAsync(token);
            if (user is null)
            {
                return NotFound("Пользователь не найден");
            }

            var res = await reservationRepository.GetNextReservationAsync(user.Id);
            if (res is null)
            {
                return Ok();
            }

            var response = new GetNextReservationResponse
            {
                ActivityId = res.Slot.ActivityId,
                Name = res.Slot.Activity.ActivityName,
                ImageUrl = res.Slot.Activity.ImageUrl,
                DateTime = res.Slot.SlotDateTime,
                AddressString = $"{res.Slot.Activity.Location?.AddressCity ?? ""} {res.Slot.Activity.Location?.AddressStreet ?? ""} {res.Slot.Activity.Location?.AddressBuildingNumber ?? ""}",
                Coordinates = new Coordinates
                {
                    Latitude = res.Slot.Activity.Location.AddressLatitude,
                    Longitude = res.Slot.Activity.Location.AddressLongitude
                },
                LocationName = res.Slot.Activity.Location.LocationName,
                ReservationId = res.Id,
                Status = res.Status

            };
            return Ok(response);

        }
        [HttpGet(Name = "GetReservation")]
        public async Task<IActionResult> GetReservation(int reservationId)
        {

            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Отсутствует токен доступа");
            }
            var user = await userRepository.GetByAccessTokenAsync(token);
            if (user is null)
            {
                return NotFound("Пользователь не найден");
            }

            var res = await reservationRepository.GetByIdAsync(reservationId);
            if (res is null)
            {
                return BadRequest($"Брони с id {reservationId} не существует");
            }
            if (res.UserId != user.Id)
            {
                return BadRequest("Данная бронь принадлежит другому пользователю");
            }

            var response = new GetReservationResponse
            {
                ActivityId = res.Slot.ActivityId,
                ReservationId = reservationId,
                AccessCode = res.AccessCode,
                DateTime = res.Slot.SlotDateTime,
                SlotId = res.SlotId,
                Count = res.Count,
                Status = res.Status,

            };
            return Ok(response);
        }
        [HttpGet(Name = "GetReservations")]
        public async Task<IActionResult> GetReservations(DateTime startDate, DateTime endDate)
        {

            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Отсутствует токен доступа");
            }
            var user = await userRepository.GetByAccessTokenAsync(token);
            if (user is null)
            {
                return NotFound("Пользователь не найден");
            }

            var reservations = (await reservationRepository.GetReservationsByUserAsync(user.Id))
                .Where(x => x.Slot.SlotDateTime >= startDate && x.Slot.SlotDateTime <= endDate);
            if (reservations is null)
            {
                return Ok("Нет броней");
            }
            var reservationsResponse = reservations.Select(x => new GetSingleReservation
            {
                Address = $"{x.Slot.Activity.Location.AddressCity} {x.Slot.Activity.Location.AddressStreet} {x.Slot.Activity.Location.AddressBuildingNumber}",
                DateTime = x.Slot.SlotDateTime,
                ImageUrl = x.Slot.Activity.ImageUrl,
                LocationName = x.Slot.Activity.Location.LocationName,
                ReservationId = x.Id,
                ActivityId = x.Slot.ActivityId,
                ActivityName = x.Slot.Activity.ActivityName,
                Coordinates = new Coordinates
                {
                    Latitude = x.Slot.Activity.Location.AddressLatitude,
                    Longitude = x.Slot.Activity.Location.AddressLongitude
                },
                Status = x.Status

            }).ToList();
            var response = new GetReservationsResponse
            {
                SelectedDateTimeStart = startDate,
                SelectedDateTimeEnd = endDate,
                Reservations = reservationsResponse

            };
            return Ok(response);
        }

        [HttpPost(Name = "SetReservationCanceledByUser")]
        public async Task<IActionResult> SetReservationCanceledByUser(int reservationId)
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Отсутствует токен доступа");
            }
            var user = await userRepository.GetByAccessTokenAsync(token);
            if (user is null)
            {
                return NotFound("Пользователь не найден");
            }

            var res = await reservationRepository.GetByIdAsync(reservationId);
            if (res is null)
            {
                return BadRequest("Брони не найдены");
            }
            if (res.UserId != user.Id)
            {
                return BadRequest("Данная бронь принадлежит другому пользователю");
            }
            if (res.Status != Status.New && res.Status != Status.Confirmed)
            {
                return BadRequest("Бронь должна быть \"New\" или \"Confirmed\"");
            }
            if (res.Slot.SlotDateTime - DateTime.Now < new TimeSpan(12, 0, 0))
            {
                return BadRequest("Нельзя отменить бронь менее, чем за 12 часов");
            }
            res.Status = Status.CanceledByUser;
            var slot = res.Slot;
            slot.FreePlaces += res.Count;
            if (canceletionTokenSources.ContainsKey(reservationId))
            {
                canceletionTokenSources[res.Id].Cancel();
            }
            await reservationRepository.UpdateAsync(res);
            await slotRepository.UpdateAsync(slot);
            return Ok();
        }
        [HttpPost(Name = "SetReservationConfirmed")]
        public async Task<IActionResult> SetReservationConfirmed(int reservationId)
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Отсутствует токен доступа");
            }
            var admin = await adminRepository.GetByAccessTokenAsync(token);
            if (admin is null)
            {
                return NotFound("Пользователь администратора не найден");
            }

            var res = await reservationRepository.GetByIdAsync(reservationId);
            if (res.Status != Status.New)
            {
                return BadRequest("Бронь не в статусе \"New\"");
            }
            res.Status = Status.Confirmed;
            await reservationRepository.UpdateAsync(res);

            var user = await userRepository.GetByIdAsync(res.UserId);
            if (user is null)
            {
                return BadRequest($"This reservation is invalid. User with id {res.UserId} not found");
            }
            var slot = res.Slot;
            if (user.FirebaseToken != null)
            {
                NotificationManager.NotifyUser("Ваша бронь подтверждена!", $"{slot.Activity.ActivityName} в {slot.Activity.Location.LocationName}\n" +
                    $"{slot.SlotDateTime.Date} в {slot.SlotDateTime.Hour}:{slot.SlotDateTime.Minute}",
                    slot.Activity.ImageUrl,
                    user.FirebaseToken,
                    DateTime.Now,
                    new CancellationToken());
            }
            return Ok();
        }
        [HttpPost(Name = "SetReservationUsed")]
        public async Task<IActionResult> SetReservationUsed(int reservationId)
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Отсутствует токен доступа");
            }
            var admin = await adminRepository.GetByAccessTokenAsync(token);
            if (admin is null)
            {
                return NotFound("Пользователь администратора не найден");
            }

            var res = await reservationRepository.GetByIdAsync(reservationId);
            if (res.Status != Status.Confirmed)
            {
                return BadRequest("Бронь не в статусе \"Confirmed\"");
            }
            res.Status = Status.Used;
            await reservationRepository.UpdateAsync(res);
            return Ok();
        }

        [HttpPost(Name = "SetReservationMissed")]
        public async Task<IActionResult> SetReservationMissed(int reservationId)
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Отсутствует токен доступа");
            }
            var admin = await adminRepository.GetByAccessTokenAsync(token);
            if (admin is null)
            {
                return NotFound("Пользователь администратора не найден");
            }

            var res = await reservationRepository.GetByIdAsync(reservationId);
            if (res.Status != Status.Confirmed)
            {
                return BadRequest("Бронь не в статусе \"Confirmed\"");
            }
            res.Status = Status.Missed;
            await reservationRepository.UpdateAsync(res);
            return Ok();
        }
        [HttpPost(Name = "SetReservationCanceledByAdmin")]
        public async Task<IActionResult> SetReservationCanceledByAdmin(int reservationId)
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Отсутствует токен доступа");
            }
            var admin = await adminRepository.GetByAccessTokenAsync(token);
            if (admin is null)
            {
                return NotFound("Пользователь администратора не найден");
            }

            var res = await reservationRepository.GetByIdAsync(reservationId);
            if (res.Status != Status.New && res.Status != Status.Confirmed)
            {
                return BadRequest("Бронь не в статусе \"New\" или \"Confirmed\"");
            }
            res.Status = Status.CanceledByAdmin;
            var slot = res.Slot;
            slot.FreePlaces += res.Count;

            await reservationRepository.UpdateAsync(res);
            await slotRepository.UpdateAsync(slot);

            var user = await userRepository.GetByIdAsync(res.UserId);
            if (user is null)
            {
                return BadRequest($"This reservation is invalid. User with id {res.UserId} not found");
            }
            if (user.FirebaseToken != null)
            {
                NotificationManager.NotifyUser("Ваша бронь отменена администратором!", $"{slot.Activity.ActivityName} в {slot.Activity.Location.LocationName}\n" +
                    $"{slot.SlotDateTime.Date} в {slot.SlotDateTime.Hour}:{slot.SlotDateTime.Minute}",
                    slot.Activity.ImageUrl,
                    user.FirebaseToken,
                    DateTime.Now,
                    new CancellationToken());
                if (canceletionTokenSources.ContainsKey(reservationId))
                {
                    canceletionTokenSources[res.Id].Cancel();
                }
            }
            return Ok();
        }

        private async Task _setReservationMissed(int reservationId, DateTime time,   CancellationToken token)
        {
            if (time > DateTime.Now)
            {
                await Task.Delay(time - DateTime.Now + new TimeSpan(0, 0, 15), token);
            }
            var res = await reservationRepository.GetByIdAsync(reservationId);
            if (res != null)
            {
                if (res.Status == Status.New || res.Status == Status.Confirmed)
                {
                    res.Status = Status.Missed;
                    await reservationRepository.UpdateAsync(res);
                }
            }
        }
    }
}