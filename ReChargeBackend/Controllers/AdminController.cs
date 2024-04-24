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
        /*
        [HttpGet(Name = "GetUserByNumberTest")]
        public AdminUser GetUserByNumberTest(string number)
        {
            return adminRepository.GetByNumber(number);
        }

        [HttpGet(Name = "GetUserByAccessToken")]
        public IActionResult GetUserByAccessToken()
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
            return Ok(new GetUserByTokenResponse
            {
                Name= user.Name,
                Surname = user.Surname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });
        }
        [HttpPost(Name = "LogOut")]
        public IActionResult LogOut()
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
        //TODO: аксесс токен хранить на сессиию с устройством, чтобы не логаутило
        [HttpPost(Name = "UpdateUser")]
        public IActionResult UpdateUser(UpdateUserInfoRequest request)
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Not authorized, access token required");
            }
            var user = adminRepository.GetByAccessToken(token);
            if (user == null)
            {
                return NotFound("user not found");
            }
            try
            {
                adminRepository.Update(new AdminUser()
                {
                    Email = request.Email,
                    Name = request.Name,
                    PhoneNumber = request.PhoneNumber,
                    Surname = request.Surname,
                    Id = user.Id,
                    AccessHash = user.AccessHash,
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
        */

    }
}