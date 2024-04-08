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

namespace BackendReCharge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IVerificationCodeRepository verificationCodeRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public AuthorizationController(IUserRepository userRepository, IVerificationCodeRepository verificationCodeRepository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) 
        {
            this.userRepository = userRepository;
            this.verificationCodeRepository = verificationCodeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        private readonly ILogger<UserController> _logger;

        [HttpPost(Name = "RequestCode")]
        public IActionResult AuthPhone([FromBody] PhoneAuthRequest info)
        {
            //TODO: IMPLEMENT PROPER NUMBER CHECKING
            if (Temp.IsPhoneNumberValid(info.phoneNumber))
            {
                var sessionId = Temp.GenerateSessionId();
                verificationCodeRepository.Add(new VerificationCode()
                {
                    Code = Temp.GenerateCode(),
                    PhoneNumber = info.phoneNumber,
                    SessionId = sessionId,
                });
                return Ok(new PhoneAuthResponse()
                {
                    //TODO: idk how session ids work...
                    SessionId = sessionId,
                    TitleText = "Введите код, полученный на " + info.phoneNumber,
                    CodeSize = 4,
                    ConditionalInfo = new ConditionalInfoResponse()
                    {
                        Message = "Совершая авторизацию вы соглашаетесь с правилами сервиса",
                        Url = "google.com"
                    }

                });
            }

            return BadRequest("Phone number invalid");
        }
        [HttpGet(Name = "GetConditionalInfo")]
        public IActionResult GetConditionalInfo()
        {
            return Ok(new ConditionalInfoResponse()
            {
                Message = "Совершая авторизацию вы соглашаетесь с правилами сервиса",
                Url = "google.com"
            });
        }

        [AllowAnonymous]
        [HttpPost(Name = "Auth")]
        public IActionResult Auth([FromBody] AuthRequest info)
        {
            try
            {
                var session = verificationCodeRepository.GetBySession(info.sessionId);
                if (session.Code == info.code)
                {
                    var user = userRepository.GetByNumber(info.phoneNumber);
                    string accessToken = Temp.GenerateAccessToken();
                    if (user is null)
                    {
                        if (DateTime.Now - session.CreationDateTime < new TimeSpan(0, 5, 0))
                        {
                            return BadRequest("Code has expired");
                        }
                        userRepository.Add(new User()
                        {
                            PhoneNumber = info.phoneNumber,
                            AccessHash = Utility.Hasher.Encrypt(accessToken)
                        });
                    } else
                    {
                        user.AccessHash = Utility.Hasher.Encrypt(accessToken);
                        userRepository.Update(user);
                    }
                    IdentityUser? identityUser = userManager.Users.FirstOrDefault(x => x.PhoneNumber == info.phoneNumber);
                    if (identityUser is null)
                    {
                        identityUser = new IdentityUser(info.phoneNumber)
                        {
                            PhoneNumber = info.phoneNumber,
                            PhoneNumberConfirmed = true,
                        };

                        userManager.CreateAsync(identityUser).Wait();
                        signInManager.SignOutAsync().Wait();
                        signInManager.SignInAsync(identityUser, isPersistent: false).Wait();
                        identityUser = userManager.Users.FirstOrDefault(x => x.PhoneNumber == info.phoneNumber);
                    }

                    return Ok(accessToken);
                }
                return BadRequest("Wrong code");
            } catch (ArgumentException e)
            {
                Console.WriteLine(e);
                Console.WriteLine(e.Message);
            }
            return BadRequest("Seesion not found");

        }


    }
}