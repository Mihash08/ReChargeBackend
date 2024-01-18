using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using ReChargeBackend.Requests;
using ReChargeBackend.Responses;
using Data.Interfaces;
using SportsStore.Data.Interfaces;
using ReChargeBackend.Utility;
using System.Linq.Expressions;

namespace BackendReCharge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IVerificationCodeRepository verificationCodeRepository;
        public UserController(IUserRepository userRepository, IVerificationCodeRepository verificationCodeRepository) 
        {
            this.userRepository = userRepository;
            this.verificationCodeRepository = verificationCodeRepository;
        }

        private readonly ILogger<UserController> _logger;

        [HttpGet(Name = "GetUsers")]
        public IEnumerable<User> GetUsers()
        {
            return userRepository.GetAll();
        }

        [HttpGet(Name = "GetUser")]
        public User GetUser(int id)
        {
            return userRepository.GetById(id);
        }

        [HttpPost(Name = "AuthPhone")]
        public PhoneAuthResponse AuthPhone([FromBody] PhoneAuthRequest info)
        {
            //TODO: IMPLEMENT PROPER NUMBER CHECKING
            if (Temp.IsPhoneNumberValid(info.phoneNumber))
            {
                verificationCodeRepository.Add(new VerificationCode()
                {
                    //TODO: this gencode is temporary
                    Code = Temp.GenerateCode(),
                    PhoneNumber = info.phoneNumber,
                    SessionId = info.sessionId,
                });
                return new PhoneAuthResponse()
                {
                    isSuccess = true,
                    //TODO: idk how session ids work...
                    sessionId = info.sessionId,
                    titleText = "Введите код, полученный на " + info.phoneNumber,
                    codeSize = 4,
                    conditionalInfo = new ConditionalInfoResponse()
                    {
                        //TODO: Part of the text is supposed to be a link. How is this going to be implemented?
                        message = "Совершая авторизацию вы соглашаетесь с правилами сервиса",
                        url = "google.com"
                    }

                };
            }

            return new PhoneAuthResponse()
            {
                isSuccess = false,
            };
        }
        [HttpGet(Name = "GetConditionalInfo")]
        public ConditionalInfoResponse GetConditionalInfo()
        {
            return new ConditionalInfoResponse()
            {
                message = "Совершая авторизацию вы соглашаетесь с правилами сервиса",
                url = "google.com"
            };
        }
        [HttpPost(Name = "Auth")]
        public AuthResponse Auth([FromBody] AuthRequest info)
        {
            try
            {
                var session = verificationCodeRepository.GetBySession(info.sessionId);
                if (session.Code == info.code)
                {
                    userRepository.Add(new User()
                    {
                        PhoneNumber = info.phoneNumber
                    });
                    return new AuthResponse()
                    {
                        isSuccess = true,
                        //TODO: this is a placeholder, replace with actual accessToken
                        accessToken = "",
                    };
                }
                return new AuthResponse()
                {
                    isSuccess = false,
                    message = "Wrong code"
                };
            } catch (ArgumentException e)
            {
                Console.WriteLine(e);
                Console.WriteLine(e.Message);
            }
            return new AuthResponse()
            {
                isSuccess = false,
                message = "Session error"
            };

        }
        [HttpGet(Name = "GetUserByNumber")]
        public User GetUserByNumber(string number)
        {
            return userRepository.GetByNumber(number);
        }


    }
}