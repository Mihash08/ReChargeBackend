using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using ReChargeBackend.Requests;
using ReChargeBackend.Responses;
using Data.Interfaces;
using ReCharge.Data.Interfaces;
using ReChargeBackend.Utility;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using Utility;
using Microsoft.Extensions.Primitives;
using Data.Repositories;

namespace BackendReCharge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminUserRepository adminRepository;
        private readonly IVerificationCodeRepository verificationCodeRepository;
        private readonly IReservationRepository reservationRepository;
        private readonly IUserRepository userRepository;
        private readonly ISlotRepository slotRepository;
        public AdminController(IAdminUserRepository adminRepository,
            IVerificationCodeRepository verificationCodeRepository,
            IReservationRepository reservationRepository,
            IUserRepository userRepository,
            ISlotRepository slotRepository)
        {
            this.adminRepository = adminRepository;
            this.verificationCodeRepository = verificationCodeRepository;
            this.reservationRepository = reservationRepository;
            this.userRepository = userRepository;
            this.slotRepository = slotRepository;
        }

        private readonly ILogger<UserController> _logger;
        [HttpPost(Name = "AdminLogOut")]
        public async Task<IActionResult> AdminLogOut()
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
            admin.AccessHash = null;
            await adminRepository.UpdateAsync(admin);
            return Ok();
        }



        [HttpPost(Name = "AdminAuth")]
        public async Task<IActionResult> AdminAuth([FromBody] AuthRequest info)
        {
            try
            {
                var session = await verificationCodeRepository.GetBySessionAsync(info.sessionId);
                if (Hasher.Verify(info.code, session.Code))
                {
                    var admin = await adminRepository.GetByNumberAsync(session.PhoneNumber);
                    string accessToken = Temp.GenerateAccessToken();
                    if (DateTime.Now - session.CreationDateTime < new TimeSpan(0, 5, 0))
                    {
                        verificationCodeRepository.DeleteAsync(session);
                        return BadRequest("Время действия кода истекло");
                    }
                    if (admin is null)
                    {
                        return BadRequest("Администратор не найден");
                    }
                    else
                    {
                        admin.AccessHash = Hasher.Encrypt(accessToken);
                        await adminRepository.UpdateAsync(admin);
                    }
                    await verificationCodeRepository.DeleteAsync(session);
                    return Ok(new AuthResponse { AccessToken = accessToken });
                }
                return BadRequest("Неправильный код");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                Console.WriteLine(e.Message);
            }
            return BadRequest("Сессия не найдена");

        }
        [HttpPost(Name = "AuthPhoneAdmind")]
        public async Task<IActionResult> AdminAuthPhone([FromBody] PhoneAuthRequest info)
        {
            //TODO: IMPLEMENT PROPER NUMBER CHECKING
            if (Temp.IsPhoneNumberValid(info.phoneNumber))
            {
                var sessionId = Temp.GenerateSessionId();
                //var code = Temp.GenerateCode();
                var code = "12345";
                await verificationCodeRepository.AddAsync(new VerificationCode()
                {
                    Code = Hasher.Encrypt(code),
                    PhoneNumber = info.phoneNumber,
                    SessionId = sessionId,
                });
                //TODO: send code to phone
                Console.WriteLine(code);
                return Ok(new PhoneAuthResponse()
                {
                    SessionId = sessionId,
                    TitleText = "Введите полученный код",
                    CodeSize = 5,
                    ConditionalInfo = new ConditionalInfoResponse()
                    {
                        Message = "Совершая авторизацию,\nвы соглашаетесь с правилами сервиса",
                        Url = "google.com"
                    }

                });
            }

            return BadRequest("Невалидный номер телефона");
        }

        [HttpPost(Name = "SetAdminFirebaseToken")]
        public async Task<IActionResult> SetAdminFirebaseToken(string firebaseToken)
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Отсутствует токен доступа");
            }
            var admin = await adminRepository.GetByAccessTokenAsync(token);
            if (admin is null)
            {
                return NotFound("Пользователь не найден");
            }
            admin.FirebaseToken = firebaseToken;
            try
            {
                await adminRepository.UpdateAsync(admin);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Name = "VerifyCode")]
        public async Task<IActionResult> VerifyCode(string code)
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

            var res = await reservationRepository.GetReservationByCodeAsync(code);
            if (res is null)
            {
                return BadRequest("Неверный код");
            }
            if (res.Status == Status.New)
            {
                return BadRequest("Бронь не подтверждена");
            }
            if (res.Status == Status.Used || res.Status == Status.Missed)
            {
                return BadRequest("Бронь уже прошла");
            }
            if (res.Status == Status.CanceledByUser || res.Status == Status.CanceledByAdmin)
            {
                return BadRequest("Бронь отменена");
            }
            return Ok(new VerifyCodeResponse
            {
                ReservationId = res.Id,
                ActivityName = res.Slot.Activity.ActivityName,
                DurationInMinutes = res.Slot.LengthMinutes,
                NumberOfUsers = res.Count,
                ReservationStartTime = res.Slot.SlotDateTime,
                TotalPrice = res.Count * res.Slot.Price,
                Username = res.Name
            });
        }
        [HttpGet(Name = "GetLocationReservations")]
        public async Task<IActionResult> GetLocationReservations()
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

            var reses = await reservationRepository.GetReservationsByLocationAsync(admin.LocationId);
            return Ok(reses.Select(x => new GetLocationReservationsResponse
            {
                ActivityName = x.Name,
                ReservationCount = x.Count,
                ReservationId = x.Id,
                SlotTime = x.Slot.SlotDateTime,
                Status = x.Slot.SlotDateTime.AddMinutes(x.Slot.LengthMinutes) < DateTime.Now ? x.Status == Status.New ? Status.CanceledByAdmin : Status.Missed : x.Status,
                TotalPrice = x.Count * x.Slot.Price
            }));

        }
        [HttpGet(Name = "GetLocationNewReservations")]
        public async Task<IActionResult> GetLocationNewReservations()
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

            var reses = (await reservationRepository.GetReservationsByLocationAsync(admin.LocationId)).Where(x => x.Status == Status.New)
                .Where(x => x.Slot.SlotDateTime.AddMinutes(x.Slot.LengthMinutes) > DateTime.Now);
            return Ok(reses.Select(x => new GetLocationReservationsResponse
            {
                ActivityName = x.Name,
                ReservationCount = x.Count,
                ReservationId = x.Id,
                SlotTime = x.Slot.SlotDateTime,
                Status = x.Status,
                TotalPrice = x.Count * x.Slot.Price
            }));

        }

        
    }
}