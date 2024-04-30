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

namespace BackendReCharge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IVerificationCodeRepository verificationCodeRepository;
        private readonly IReservationRepository reservationRepository;
        public UserController(IUserRepository userRepository, 
            IVerificationCodeRepository verificationCodeRepository, 
            IReservationRepository reservationRepository) 
        {
            this.userRepository = userRepository;
            this.verificationCodeRepository = verificationCodeRepository;
            this.reservationRepository = reservationRepository;
        }

        private readonly ILogger<UserController> _logger;

        [HttpGet(Name = "GetProfileHeader")]
        public IActionResult GetProfileHeader()
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Отсутствует токен доступа");
            }
            var user = userRepository.GetByAccessToken(token);
            if (user is null)
            {
                return NotFound("Пользователь не найден");
            }

            return Ok(new GetProfileHeaderResponse()
            {
                Name = user.Name,
                PhotoUrl = user.ImageUrl,
            });
        }
        [HttpGet(Name = "GetUserByNumberTest")]
        public User GetUserByNumberTest(string number)
        {
            return userRepository.GetByNumber(number);
        }

        [HttpGet(Name = "GetUserByAccessToken")]
        public IActionResult GetUserByAccessToken()
        {

            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Отсутствует токен доступа");
            }
            var user = userRepository.GetByAccessToken(token);
            if (user is null)
            {
                return NotFound("Пользователь не найден");
            }
            return Ok(new GetUserByTokenResponse
            {
                Name= user.Name,
                Surname = user.Surname,
                Email = user.Email,
                BirthDate = user.BirthDate,
                City = user.City,
                Gender = user.Gender,
                ImageUrl = user.ImageUrl,
                PhoneNumber = user.PhoneNumber
            });
        }
        [HttpPost(Name = "LogOut")]
        public IActionResult LogOut()
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Отсутствует токен доступа");
            }
            var user = userRepository.GetByAccessToken(token);
            if (user is null)
            {
                return NotFound("Пользователь не найден");
            }
            user.AccessHash = null;
            userRepository.Update(user);
            return Ok();
        }
        //TODO: аксесс токен хранить на сессиию с устройством, чтобы не логаутило
        [HttpPost(Name = "UpdateUser")]
        public IActionResult UpdateUser(UpdateUserInfoRequest request)
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Отсутствует токен доступа");
            }
            var user = userRepository.GetByAccessToken(token);
            if (user is null)
            {
                return NotFound("Пользователь не найден");
            }
            try
            {
                userRepository.Update(new User()
                {
                    BirthDate = request.BirthDate,
                    Email = request.Email,
                    Name = request.Name,
                    PhoneNumber = request.PhoneNumber,
                    Surname = request.Surname,
                    ImageUrl = request.ImageUrl,
                    Id = user.Id,
                    City = request.City,
                    AccessHash = user.AccessHash,
                    Gender = request.Gender,
                });
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }


    }
}