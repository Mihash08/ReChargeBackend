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
        public AdminController(IAdminUserRepository adminRepository,
            IVerificationCodeRepository verificationCodeRepository,
            IReservationRepository reservationRepository)
        {
            this.adminRepository = adminRepository;
            this.verificationCodeRepository = verificationCodeRepository;
            this.reservationRepository = reservationRepository;
        }

        private readonly ILogger<UserController> _logger;
        [HttpPost(Name = "AdminLogOut")]
        public IActionResult AdminLogOut()
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Not authorized, access token required");
            }
            var user = adminRepository.GetByAccessToken(token);
            if (user is null)
            {
                return NotFound("User not found");
            }
            user.AccessHash = null;
            adminRepository.Update(user);
            return Ok();
        }



        [HttpPost(Name = "AdminAuth")]
        public IActionResult AdminAuth([FromBody] AuthRequest info)
        {
            try
            {
                var session = verificationCodeRepository.GetBySession(info.sessionId);
                if (Hasher.Verify(info.code, session.Code))
                {
                    var admin = adminRepository.GetByNumber(session.PhoneNumber);
                    string accessToken = Temp.GenerateAccessToken();
                    if (DateTime.Now - session.CreationDateTime < new TimeSpan(0, 5, 0))
                    {
                        verificationCodeRepository.Delete(session);
                        return BadRequest("Время действия кода истекло");
                    }
                    if (admin is null)
                    {
                        return BadRequest("This admin doesn't exist");
                    }
                    else
                    {
                        admin.AccessHash = Hasher.Encrypt(accessToken);
                        adminRepository.Update(admin);
                    }
                    verificationCodeRepository.Delete(session);
                    return Ok(new AuthResponse { AccessToken = accessToken });
                }
                return BadRequest("Неправильный код");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                Console.WriteLine(e.Message);
            }
            return BadRequest("Seesion not found");

        }
        [HttpPost(Name = "AuthPhoneAdmind")]
        public IActionResult AdminAuthPhone([FromBody] PhoneAuthRequest info)
        {
            //TODO: IMPLEMENT PROPER NUMBER CHECKING
            if (Temp.IsPhoneNumberValid(info.phoneNumber))
            {
                var sessionId = Temp.GenerateSessionId();
                //var code = Temp.GenerateCode();
                var code = "12345";
                verificationCodeRepository.Add(new VerificationCode()
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

            return BadRequest("Phone number invalid");
        }

        [HttpPost(Name = "VerifyCode")]
        public IActionResult VerifyCode(string code)
        {

            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Not authorized, access token required");
            }
            var admin = adminRepository.GetByAccessToken(token);
            if (admin is null)
            {
                return NotFound("Admin not found");
            }
            
            var res = reservationRepository.GetReservationByCode(code);
            if (res is null)
            {
                return BadRequest("Invalid Code");
            }
            if (res.Status == Status.New)
            {
                return BadRequest("Reservation is not confirmed");
            }
            if (res.Status == Status.Used || res.Status == Status.Missed)
            {
                return BadRequest("This reservation is over");
            }
            if (res.Status == Status.CanceledByUser || res.Status == Status.CanceledByAdmin)
            {
                return BadRequest("This reservation is canceled");
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
        public IActionResult GetLocationReservations()
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Not authorized, access token required");
            }
            var admin = adminRepository.GetByAccessToken(token);
            if (admin is null)
            {
                return NotFound("Admin not found");
            }

            var reses = reservationRepository.GetReservationsByLocation(admin.LocationId);
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
        [HttpGet(Name = "GetLocationNewReservations")]
        public IActionResult GetLocationNewReservations()
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Not authorized, access token required");
            }
            var admin = adminRepository.GetByAccessToken(token);
            if (admin is null)
            {
                return NotFound("Admin not found");
            }

            var reses = reservationRepository.GetReservationsByLocation(admin.LocationId).Where(x => x.Status == Status.New);
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

        [HttpPost(Name = "SetReservationConfirmed")]
        public IActionResult SetReservationConfirmed(int reservationId)
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Not authorized, access token required");
            }
            var admin = adminRepository.GetByAccessToken(token);
            if (admin is null)
            {
                return NotFound("Admin not found");
            }

            var res = reservationRepository.GetById(reservationId);
            if (res.Status != Status.New)
            {
                return BadRequest("Reservation is not in \"New\" state");
            }
            res.Status = Status.Confirmed;
            reservationRepository.Update(res);
            return Ok();
        }
        [HttpPost(Name = "SetReservationUsed")]
        public IActionResult SetReservationUsed(int reservationId)
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Not authorized, access token required");
            }
            var admin = adminRepository.GetByAccessToken(token);
            if (admin is null)
            {
                return NotFound("Admin not found");
            }

            var res = reservationRepository.GetById(reservationId);
            if (res.Status != Status.Confirmed)
            {
                return BadRequest("Reservation is not in \"Confirmed\" state");
            }
            res.Status = Status.Used;
            reservationRepository.Update(res);
            return Ok();
        }

        [HttpPost(Name = "SetReservationMissed")]
        public IActionResult SetReservationMissed(int reservationId)
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Not authorized, access token required");
            }
            var admin = adminRepository.GetByAccessToken(token);
            if (admin is null)
            {
                return NotFound("Admin not found");
            }

            var res = reservationRepository.GetById(reservationId);
            if (res.Status != Status.Confirmed)
            {
                return BadRequest("Reservation is not in \"Confirmed\" state");
            }
            res.Status = Status.Missed;
            reservationRepository.Update(res);
            return Ok();
        }
        [HttpPost(Name = "SetReservationCanceledByAdmin")]
        public IActionResult SetReservationCanceledByAdmin(int reservationId)
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Not authorized, access token required");
            }
            var admin = adminRepository.GetByAccessToken(token);
            if (admin is null)
            {
                return NotFound("Admin not found");
            }

            var res = reservationRepository.GetById(reservationId);
            if (res.Status != Status.New && res.Status != Status.Confirmed)
            {
                return BadRequest("Reservation is not in \"New\" or \"Confirmed\" state");
            }
            res.Status = Status.CanceledByAdmin;
            reservationRepository.Update(res);
            return Ok();
        }

    }
}