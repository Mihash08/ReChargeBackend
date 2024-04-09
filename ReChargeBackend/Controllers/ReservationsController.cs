using Azure.Core;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using ReCharge.Data.Interfaces;
using ReChargeBackend.Responses;
using ReChargeBackend.Utility;
using Utility;

namespace BackendReCharge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReservationsController : ControllerBase
    {
        private IReservationRepository reservationRepository;
        private IUserRepository userRepository;
        private readonly ILogger<ReservationsController> _logger;

        public ReservationsController(ILogger<ReservationsController> logger, IReservationRepository reservationRepository, IUserRepository userRepository)
        {
            _logger = logger;
            this.reservationRepository = reservationRepository;
            this.userRepository = userRepository;
        }

        [HttpGet(Name = "GetReservationsByToken")]
        public IActionResult GetReservationsByToken(string accessToken)
        {
            var user = userRepository.GetByAccessToken(accessToken);
            if (user is null)
            {
                return NotFound("User not found");
            }
            return Ok(reservationRepository.GetReservationsByUser(user.Id));
        }
        [HttpGet(Name = "GetReservation")]
        public Reservation? GetReservation(int id)
        {
            return reservationRepository.GetById(id);
        }

        [HttpGet(Name = "GetNextReservation")]
        public IActionResult GetNextReservation(string accessToken)
        {

            var user = userRepository.GetByAccessToken(accessToken);
            if (user is null)
            {
                return NotFound("User not found");
            }

            var res = reservationRepository.GetNextReservation(user.Id);
            if (res is null)
            {
                return Ok();
            }

            var response = new GetNextReservationResponse
            {
                Name = res.Slot.Activity.ActivityName,
                ImageUrl = res.Slot.Activity.ImageUrl,
                DateTime = res.Slot.SlotDateTime,
                AddressString = $"{res.Slot.Activity.Location?.AddressCity ?? ""} {res.Slot.Activity.Location?.AddressStreet ?? ""} {res.Slot.Activity.Location?.AddressBuildingNumber ?? ""}",
                Coordinates = new Coordinates
                {
                    Latitude = res.Slot.Activity.Location.AddressLatitude,
                    Longitude = res.Slot.Activity.Location.AddressLongitude
                },
                LocationName = res.Slot.Activity.Location.LocationName,
                ReservationId = 1

            };
            return Ok(response);

        }
    }
}