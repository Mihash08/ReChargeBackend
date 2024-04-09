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

//TODO: !!! add status code to all responses
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
                return BadRequest("Not authorized, access token required");
            }
            var user = userRepository.GetByAccessToken(token);
            if (user is null)
            {
                return NotFound("User not found");
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
        //TODO: аксесс токен хранить на сессиию с устройством, чтобы не логаутило
        [HttpPost(Name = "UpdateUser")]
        public IActionResult UpdateUser(UpdateUserInfoRequest request)
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Not authorized, access token required");
            }
            var user = userRepository.GetByAccessToken(token);
            if (user == null)
            {
                return NotFound("user not found");
            }
            try
            {
                userRepository.Update(new User()
                {
                    BirthDate = request.BirthDay,
                    Email = request.Email,
                    Name = request.FirstName,
                    PhoneNumber = request.Phone,
                    Surname = request.LastName,
                    ImageUrl = request.ImageURL,
                    Id = user.Id
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
    //TODO: getProfileHeader
}