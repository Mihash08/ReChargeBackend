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
        public GetProfileHeaderResponse GetProfileHeader(int userId, string accessToken)
        {
            var user = userRepository.GetById(userId);
            if (user is null)
            {
                return new GetProfileHeaderResponse()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    StatusMessage = "User not found"
                };
            }

            if (user.AccessHash is null)
            {
                return new GetProfileHeaderResponse()
                {
                    StatusCode = StatusCodes.Status203NonAuthoritative,
                    StatusMessage = "Not authorized as this user"
                };
            }
            if (Hasher.Verify(accessToken, user.AccessHash))
            {
                return new GetProfileHeaderResponse()
                {
                    Name = user.Name,
                    PhotoUrl = user.ImageUrl,
                    StatusCode = StatusCodes.Status200OK,
                };
            }
            else
            {
                return new GetProfileHeaderResponse()
                {
                    StatusCode = StatusCodes.Status203NonAuthoritative,
                    StatusMessage = "Not authorized as this user"
                };
            }
        }
        [HttpGet(Name = "GetNextReservation")]
        public GetNextReservationResponse GetNextReservation(int userId, string accessToken)
        {
            var user = userRepository.GetById(userId);
            if (user is null)
            {
                return new GetNextReservationResponse()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    StatusMessage = "User not found"
                };
            }

            if (user.AccessHash is null)
            {
                return new GetNextReservationResponse()
                {
                    StatusCode = StatusCodes.Status203NonAuthoritative,
                    StatusMessage = "Not authorized as this user"
                };
            }
            if (Hasher.Verify(accessToken, user.AccessHash))
            {
                var reservation = reservationRepository.GetNextReservation(userId);
                if (reservation is null)
                {
                    return new GetNextReservationResponse()
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        StatusMessage = "Next activity not found"
                    };
                }
                return new GetNextReservationResponse()
                {
                    ReservationId = reservation.Id,
                    ActivityName = reservation.Slot.Activity.ActivityName,
                    Time = reservation.Slot.SlotDateTime,
                    Address = reservation.Slot.Activity.Location.AddressCity + " " + 
                    reservation.Slot.Activity.Location.AddressStreet + " " +
                    reservation.Slot.Activity.Location.AddressBuildingNumber,
                    coordinates = new Coordinates()
                    {
                        Latitude = reservation.Slot.Activity.Location.AddressLatitude,
                        Longitude = reservation.Slot.Activity.Location.AddressLongitude
                    },
                    ImageUrl = reservation.Slot.Activity.ImageUrl,
                    Price = reservation.Slot.Price,
                    StatusCode = StatusCodes.Status200OK
                };
            }
            else
            {
                return new GetNextReservationResponse()
                {
                    StatusCode = StatusCodes.Status203NonAuthoritative,
                    StatusMessage = "Not authorized as this user"
                };
            }
        }

        [HttpGet(Name = "GetUserByNumber")]
        public User GetUserByNumber(string number)
        {
            return userRepository.GetByNumber(number);
        }
        //TODO: аксесс токен хранить на сессиию с устройством, чтобы не логаутило
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
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }


    }
    //TODO: getProfileHeader
}