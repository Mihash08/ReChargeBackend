using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using ReChargeBackend.Requests;
using ReChargeBackend.Responses;
using Data.Interfaces;
using ReCharge.Data.Interfaces;
using ReChargeBackend.Utility;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Utility;
using Microsoft.EntityFrameworkCore.Query;

namespace BackendReCharge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IVerificationCodeRepository verificationCodeRepository;
        public AuthorizationController(IUserRepository userRepository, IVerificationCodeRepository verificationCodeRepository) 
        {
            this.userRepository = userRepository;
            this.verificationCodeRepository = verificationCodeRepository;
        }

        private readonly ILogger<UserController> _logger;

        [HttpPost(Name = "RequestCode")]
        public async Task<IActionResult> AuthPhone([FromBody] PhoneAuthRequest info)
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
                        Url = "https://docs.google.com/document/d/1adkUUJo3Grd6-75DGmmBRKPlOO-x-KYXsvnUHu0F4IY/edit?usp=sharing"
                    }

                });
            }

            return BadRequest("Невалидный номер телефона");
        }
        [HttpGet(Name = "GetConditionalInfo")]
        public IActionResult GetConditionalInfo()
        {
            return Ok(new ConditionalInfoResponse()
            {
                Message = "Совершая авторизацию вы соглашаетесь с правилами сервиса",
                Url = "https://docs.google.com/document/d/1adkUUJo3Grd6-75DGmmBRKPlOO-x-KYXsvnUHu0F4IY/edit?usp=sharing"
            });
        }

        [HttpPost(Name = "Auth")]
        public async Task<IActionResult> Auth([FromBody] AuthRequest info)
        {
            try
            {
                var session = await verificationCodeRepository.GetBySessionAsync(info.sessionId);
                if (Hasher.Verify(info.code, session.Code))
                {
                    var user = await userRepository.GetByNumberAsync(session.PhoneNumber);
                    string accessToken = Temp.GenerateAccessToken();
                    if (user is null)
                    {
                        if (DateTime.Now - session.CreationDateTime < new TimeSpan(0, 5, 0))
                        {
                            return BadRequest("Время действия кода истекло");
                        }
                        await userRepository.AddAsync(new User()
                        {
                            PhoneNumber = session.PhoneNumber,
                            AccessHash = Hasher.Encrypt(accessToken),
                            ImageUrl = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png",
                        });
                    } else
                    {
                        user.AccessHash = Hasher.Encrypt(accessToken);
                        await userRepository.UpdateAsync(user);
                    }
                    await verificationCodeRepository.DeleteAsync(session);
                    return Ok(new AuthResponse { AccessToken = accessToken});
                }
                return BadRequest("Неправильный код");
            } catch (ArgumentException e)
            {
                Console.WriteLine(e);
                Console.WriteLine(e.Message);
            }
            return BadRequest("Сессия не найдена");

        }


    }
}