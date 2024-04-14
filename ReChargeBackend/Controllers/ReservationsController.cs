using Azure.Core;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
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
        private IReservationRepository reservationRepository;
        private IUserRepository userRepository;
        private ISlotRepository slotRepository;
        private readonly ILogger<ReservationsController> _logger;

        public ReservationsController(ILogger<ReservationsController> logger, 
            IReservationRepository reservationRepository, 
            IUserRepository userRepository,
            ISlotRepository slotRepository)
        {
            _logger = logger;
            this.reservationRepository = reservationRepository;
            this.userRepository = userRepository;
            this.slotRepository = slotRepository;
        }

        [HttpGet(Name = "GetReservationsByToken")]
        public IActionResult GetReservationsByToken()
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Not authorized, access token required");
            }
            var user = userRepository.GetByAccessToken(token);
            if (user is null)
            {
                return NotFound("User not found (or invalid token)");
            }
            return Ok(reservationRepository.GetReservationsByUser(user.Id));
        }

        [HttpPost(Name = "MakeReservation")]
        public IActionResult MakeReservation(int slotId, [FromBody] MakeReservationRequest request)
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Not authorized, access token required");
            }
            var user = userRepository.GetByAccessToken(token);
            if (user is null)
            {
                return NotFound("User not found (or invalid token)");
            }
            var slot = slotRepository.GetById(slotId);
            if (slot is null)
            {
                return BadRequest($"Slot with id {slotId} not found");
            }
            if (reservationRepository.GetReservationsByUser(user.Id).Any(x=> x.SlotId == slot.Id))
            {
                return StatusCode(452, "Вы уже записаны на это занятие");
            }
            if (slot.FreePlaces >= request.ReserveCount)
            {
                reservationRepository.Add(new Reservation
                {
                    IsOver = false,
                    Slot = slot,
                    SlotId = slotId,
                    User = user,
                    UserId = user.Id,
                    Count = request.ReserveCount,
                    Email = request.Email,
                    Name = request.Name,
                    PhoneNumber = request.Phone
                });
                slot.FreePlaces -= request.ReserveCount;
                slotRepository.Update(slot);
                return Ok();
            }
            return BadRequest($"Not enough free spaces in slot ({slot.FreePlaces} spaces available");

        }
        [HttpGet(Name = "GetNextReservation")]
        public IActionResult GetNextReservation()
        {

            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Not authorized, access token required");
            }
            var user = userRepository.GetByAccessToken(token);
            if (user is null)
            {
                return NotFound("User not found");
            }

            var res = reservationRepository.GetNextReservation(user.Id);
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
                ReservationId = 1

            };
            return Ok(response);

        }
        [HttpGet(Name = "GetReservation")]
        public IActionResult GetReservation(int reservationId)
        {

            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Not authorized, access token required");
            }
            var user = userRepository.GetByAccessToken(token);
            if (user is null)
            {
                return NotFound("User not found");
            }

            var res = reservationRepository.GetById(reservationId);
            if (res is null)
            {
                return BadRequest($"Reservation with id {reservationId} doesn't exist");
            }

            var response = new GetReservationResponse
            {
                SlotId = res.SlotId,
                Count = res.Count,
                Email = res.Email,
                Name = res.Name,
                PhoneNumber = res.PhoneNumber

            };
            return Ok(response);
        }
        [HttpGet(Name = "GetReservations")]
        public IActionResult GetReservations(DateTime startDate, DateTime endDate)
        {

            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Not authorized, access token required");
            }
            var user = userRepository.GetByAccessToken(token);
            if (user is null)
            {
                return NotFound("User not found");
            }

            var reservations = reservationRepository.GetReservationsByUser(user.Id);
            if (reservations is null)
            {
                return Ok("No reservations");
            }
            var reservationsResponse = reservations.Select(x => new GetSingleReservation
            {
                Address = $"{x.Slot.Activity.Location.AddressCity} {x.Slot.Activity.Location.AddressStreet} {x.Slot.Activity.Location.AddressBuildingNumber}",
                DateTime = x.Slot.SlotDateTime,
                ImageUrl = x.Slot.Activity.ImageUrl,
                LocationName = x.Slot.Activity.Location.LocationName,
                ReservationId = x.Id

            }).ToList();
            var response = new GetReservationsResponse
            {
                SelectedDateTimeStart = startDate,
                SelectedDateTimeEnd = endDate,
                Reservations =  reservationsResponse

            };
            return Ok(response);
        }
    }
}