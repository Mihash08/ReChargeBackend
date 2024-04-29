﻿using Azure.Core;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using ReCharge.Data.Interfaces;
using ReChargeBackend.Requests;
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
        private ISlotRepository slotRepository;
        private readonly ILogger<ReservationsController> _logger;

        public ReservationsController(ILogger<ReservationsController> logger,
            IReservationRepository reservationRepository,
            IUserRepository userRepository,
            ISlotRepository slotRepository)
        {
            _logger = logger;
            this.reservationRepository = reservationRepository;
            this.userRepository = userRepository;
            this.slotRepository = slotRepository;
        }

        [HttpPost(Name = "MakeReservation")]
        public IActionResult MakeReservation(int slotId, [FromBody] MakeReservationRequest request)
        {
            StringValues token = string.Empty;
            if (!Request.Headers.TryGetValue("accessToken", out token))
            {
                return BadRequest("Not authorized, access token required");
            }
            var user = userRepository.GetByAccessToken(token);
            if (user is null)
            {
                return NotFound("User not found (or invalid token)");
            }
            var slot = slotRepository.GetById(slotId);
            if (slot is null)
            {
                return BadRequest($"Slot with id {slotId} not found");
            }
            if (reservationRepository.GetReservationsByUser(user.Id).Any(x => x.SlotId == slot.Id))
            {
                return StatusCode(452, "Вы уже записаны на это занятие");
            }
            if (slot.FreePlaces >= request.ReserveCount)
            {
                reservationRepository.Add(new Reservation
                {
                    IsOver = false,
                    Slot = slot,
                    SlotId = slotId,
                    User = user,
                    UserId = user.Id,
                    Count = request.ReserveCount,
                    Email = request.Email,
                    Name = request.Name,
                    PhoneNumber = request.Phone,
                    AccessCode = Temp.GenerateAccessCode(),
                    Status = Status.New
                });
                slot.FreePlaces -= request.ReserveCount;
                slotRepository.Update(slot);
                return Ok();
            }
            return BadRequest($"Not enough free spaces in slot ({slot.FreePlaces} spaces available");

        }
        [HttpGet(Name = "GetNextReservation")]
        public IActionResult GetNextReservation()
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

            var res = reservationRepository.GetNextReservation(user.Id);
            if (res is null)
            {
                return Ok();
            }

            var response = new GetNextReservationResponse
            {
                ActivityId = res.Slot.ActivityId,
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
                ReservationId = res.Id,
                State = res.Status

            };
            return Ok(response);

        }
        [HttpGet(Name = "GetReservation")]
        public IActionResult GetReservation(int reservationId)
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

            var res = reservationRepository.GetById(reservationId);
            if (res is null)
            {
                return BadRequest($"Reservation with id {reservationId} doesn't exist");
            }
            if (res.UserId != user.Id)
            {
                return BadRequest($"ACCESS ERROR: this reservation is from another user");
            }

                var response = new GetReservationResponse
            {
                ActivityId = res.Slot.ActivityId,
                ReservationId = reservationId,
                AccessCode = res.AccessCode,
                DateTime = res.Slot.SlotDateTime,
                SlotId = res.SlotId,
                Count = res.Count,
                State = res.Status,

            };
            return Ok(response);
        }
        [HttpGet(Name = "GetReservations")]
        public IActionResult GetReservations(DateTime startDate, DateTime endDate)
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

            var reservations = reservationRepository.GetReservationsByUser(user.Id)
                .Where(x => x.Slot.SlotDateTime >= startDate && x.Slot.SlotDateTime <= endDate);
            if (reservations is null)
            {
                return Ok("No reservations");
            }
            var reservationsResponse = reservations.Select(x => new GetSingleReservation
            {
                Address = $"{x.Slot.Activity.Location.AddressCity} {x.Slot.Activity.Location.AddressStreet} {x.Slot.Activity.Location.AddressBuildingNumber}",
                DateTime = x.Slot.SlotDateTime,
                ImageUrl = x.Slot.Activity.ImageUrl,
                LocationName = x.Slot.Activity.Location.LocationName,
                ReservationId = x.Id,
                ActivityId = x.Slot.ActivityId,
                ActivityName = x.Slot.Activity.ActivityName,
                Coordinates = new Coordinates
                {
                    Latitude = x.Slot.Activity.Location.AddressLatitude,
                    Longitude = x.Slot.Activity.Location.AddressLongitude
                },
                State = x.Status

            }).ToList();
            var response = new GetReservationsResponse
            {
                SelectedDateTimeStart = startDate,
                SelectedDateTimeEnd = endDate,
                Reservations = reservationsResponse

            };
            return Ok(response);
        }

        [HttpPost(Name = "SetReservationCanceledByUser")]
        public IActionResult SetReservationCanceledByUser(int reservationId)
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

            var res = reservationRepository.GetById(reservationId);
            if (res.Status != Status.New && res.Status != Status.Confirmed)
            {
                return BadRequest("Reservation is not in \"New\" or \"Confirmed\" state");
            }
            res.Status = Status.CanceledByUser;
            reservationRepository.Update(res);
            return Ok();
        }
    }
}