using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ReChargeBackend.Requests;
using ReChargeBackend.Responses;
using System;

namespace BackendReCharge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SlotController : ControllerBase
    {
        private readonly ISlotRepository slotRepository;
        private readonly ICategoryRepository categoryRepository;
       
        private readonly ILogger<SlotController> _logger;

        public SlotController(ILogger<SlotController> logger, 
            ISlotRepository slotRepository,
            ICategoryRepository categoryRepository)
        {
            _logger = logger;
            this.slotRepository = slotRepository;
            this.categoryRepository = categoryRepository;
        }

        [HttpGet(Name = "GetSlotsByCategory")]
        public IActionResult GetSlotsByCategoryIdAndTime(int categoryId, DateTime dateTime)
        {
            var slots = slotRepository.GetSlotsByCategoryIdAndTime(categoryId, dateTime);
            var catName = categoryRepository.GetById(categoryId).Name;
            List<GetSlotByCategoryAndDateResponse> resp = slots
                .Where(x => x.SlotDateTime > DateTime.Now)
                .Select(x => new GetSlotByCategoryAndDateResponse
            {
                SlotId = x.Id,
                ActivityName = x.Activity.ActivityName,
                Address = $"{x.Activity.Location.AddressCity} {x.Activity.Location.AddressStreet} {x.Activity.Location.AddressBuildingNumber}",
                Coordinates = new ReChargeBackend.Utility.Coordinates
                {
                    Latitude = x.Activity.Location.AddressLatitude,
                    Longitude = x.Activity.Location.AddressLongitude
                },
                DateTime = x.SlotDateTime,
                ActivityId = x.ActivityId,
                ImageUrl = x.Activity.ImageUrl,
                LengthMinutes = x.LengthMinutes,
                LocationName = x.Activity.Location.LocationName,
                Price = x.Price
            }).OrderBy(x => x.DateTime).ToList();

            return Ok(new GetSlotsByCategoryAndDateResponse
            {
                CategoryName = catName,
                Slots = resp.ToList(),
                dateTime = dateTime
            });
        }
        [HttpGet(Name = "GetSlotsByActivityIdAndTimeTest")]
        public IActionResult GetSlotsByActivityIdAndTimeTest(int activityId, DateTime dateTime)
        {
            return Ok(slotRepository.GetSlotsByActivityIdAndTime(activityId, dateTime)
                .Where(x => x.SlotDateTime > DateTime.Now).OrderBy(x => x.SlotDateTime));
        }
        [HttpGet(Name = "GetSlotTest")]
        public Slot GetSlot(int id)
        {
            return slotRepository.GetById(id);
        }
        [HttpGet(Name = "GetActivityViewSlots")]
        public IActionResult GetActivityViewSlots(int activityId, DateTime dateTime)
        {
            var slots = slotRepository.GetAllByActivityId(activityId)
                .Where(x => x.SlotDateTime.Date > dateTime && x.SlotDateTime.Date < dateTime.AddHours(24))
                .Where(x => x.SlotDateTime > DateTime.Now).OrderBy(x => x.SlotDateTime);
            return Ok(new GetActivityViewSlotsResponse
            {
                Slots = slots.Select(x =>
                new SlotView
                {
                    DurationMinutes = x.LengthMinutes,
                    Price = x.Price,
                    SlotId = x.Id,
                    StartTime =
                    x.SlotDateTime
                }).ToArray(),
                DateTime = dateTime
            });
        }
        [HttpGet(Name = "GetSlotFreeSpots")]
        public IActionResult GetSlotFreeSpots(int id)
        {
            var slot = slotRepository.GetById(id);
            if (slot == null)
            {
                return NotFound($"Slot with id {id} not found");
            }
            return Ok(new GetSlotFreeSpotsResponse {FreeSpots = slot.FreePlaces });
        }
    }
}