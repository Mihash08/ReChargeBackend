using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using ReChargeBackend.Requests;
using ReChargeBackend.Responses;
using Data.Interfaces;
using SportsStore.Data.Interfaces;
using ReChargeBackend.Utility;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using Utility;

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

        [HttpGet(Name = "GetUser")]
        public GetUserInfoResponse GetUser(int id, string accessToken)
        {
            var user = userRepository.GetById(id);
            if (user is null || user.AccessHash is null)
            {
                return new GetUserInfoResponse()
                {
                    UserId = -1,
                };
            }
            if (Hasher.Verify(accessToken, user.AccessHash))
            {
                return new GetUserInfoResponse()
                {
                    UserId = user.Id,
                    FirstName = user.Name,
                    LastName = user.Surname,
                    Phone = user.PhoneNumber,
                    Email = user.Email,
                    BirthDay = user.BirthDate,
                    Gender = user.Gender,
                };
            }
            return new GetUserInfoResponse()
            {
                UserId = -1,
            };
        }

        [HttpGet(Name = "GetUserByNumber")]
        public User GetUserByNumber(string number)
        {
            return userRepository.GetByNumber(number);
        }

        [HttpPost(Name = "UpdateUser")]
        public bool UpdateUser(UpdateUserInfoRequest request)
        {
            var user = userRepository.GetById(request.UserId);
            if (user == null)
            {
                return false;
            }
            if (!Hasher.Verify(request.accessToken, user.AccessHash))
            {
                return false;
            }
            try
            {
                userRepository.Update(new User()
                {
                    BirthDate = request.BirthDay,
                    Email = request.Email,
                    Id = request.UserId,
                    Name = request.FirstName,
                    PhoneNumber = request.Phone,
                    Surname = request.LastName,
                    ImageUrl = request.ImageURL
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}