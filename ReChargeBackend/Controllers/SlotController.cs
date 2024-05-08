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
        public async Task<IActionResult> GetSlotsByCategoryIdAndTime(int categoryId, DateTime dateTime)
        {
            var slots = await slotRepository.GetSlotsByCategoryIdAndTimeAsync(categoryId, dateTime);
            var cat = await categoryRepository.GetByIdAsync(categoryId);
            if (cat == null)
            {
                return BadRequest($"Категория с id {categoryId} не найден");
            }
            var catName = cat.Name;

            List<GetSlotByCategoryAndDateResponse> resp = slots
                .Where(x => x.SlotDateTime > DateTime.Now && x.FreePlaces > 0)
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
        public async Task<IActionResult> GetSlotsByActivityIdAndTimeTest(int activityId, DateTime dateTime)
        {
            return Ok((await slotRepository.GetSlotsByActivityIdAndTimeAsync(activityId, dateTime))
                .Where(x => x.SlotDateTime > DateTime.Now && x.FreePlaces > 0).OrderBy(x => x.SlotDateTime));
        }
        [HttpGet(Name = "GetSlotTest")]
        public async Task<IActionResult> GetSlot(int id)
        {
            var slot = await slotRepository.GetByIdAsync(id);
            if (slot is null)
            {
                return BadRequest($"Слот с id {id} не найден");
            }
            return Ok(slot);
        }
        [HttpGet(Name = "GetActivityViewSlots")]
        public async Task<IActionResult> GetActivityViewSlots(int activityId, DateTime dateTime)
        {
            var slots = (await slotRepository.GetAllByActivityIdAsync(activityId))
                .Where(x => x.SlotDateTime.Date > dateTime && x.SlotDateTime.Date < dateTime.AddHours(24))
                .Where(x => x.SlotDateTime > DateTime.Now).OrderBy(x => x.SlotDateTime);
            return Ok(new GetActivityViewSlotsResponse
            {
                Slots = slots.Where(x => x.FreePlaces > 0).Select(x =>
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
        public async Task<IActionResult> GetSlotFreeSpots(int id)
        {
            var slot = await slotRepository.GetByIdAsync(id);
            if (slot == null)
            {
                return NotFound($"Слот с id {id} не найден");
            }
            return Ok(new GetSlotFreeSpotsResponse {FreeSpots = slot.FreePlaces });
        }
    }
}