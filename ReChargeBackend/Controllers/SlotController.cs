using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ReChargeBackend.Requests;
using ReChargeBackend.Responses;

namespace BackendReCharge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SlotController : ControllerBase
    {
        //TODO: убери мок данных
        //TODO: сделай полный подсос данных
        private readonly ISlotRepository slotRepository;
        //List<Slot> slots = new List<Slot>()
        //    {
        //        new Slot()
        //        {
        //            ActivityId = 1, Id = 1, FreePlaces = 5, Price = 1500, SlotDateTime = new DateTime(2023,11,20,16,30,0)
        //        },
        //        new Slot()
        //        {
        //            ActivityId = 1, Id = 2, FreePlaces = 5, Price = 2000, SlotDateTime = new DateTime(2023,11,20,10,30,0)
        //        },
        //        new Slot()
        //        {
        //            ActivityId = 1, Id = 3, FreePlaces = 5, Price = 1250, SlotDateTime = new DateTime(2023,11,20,18,30,0)
        //        },

        //        new Slot()
        //        {
        //            ActivityId = 2, Id = 4, FreePlaces = 5, Price = 1500, SlotDateTime = new DateTime(2023,11,20,16,30,0)
        //        },
        //        new Slot()
        //        {
        //            ActivityId = 2, Id = 5, FreePlaces = 5, Price = 2000, SlotDateTime = new DateTime(2023,11,20,10,30,0)
        //        },
        //        new Slot()
        //        {
        //            ActivityId = 2, Id = 6, FreePlaces = 5, Price = 1250, SlotDateTime = new DateTime(2023,11,20,18,30,0)
        //        },

        //        new Slot()
        //        {
        //            ActivityId = 3, Id = 7, FreePlaces = 5, Price = 1500, SlotDateTime = new DateTime(2023,11,20,16,30,0)
        //        },
        //        new Slot()
        //        {
        //            ActivityId = 3, Id = 8, FreePlaces = 5, Price = 2000, SlotDateTime = new DateTime(2023,11,20,10,30,0)
        //        },
        //        new Slot()
        //        {
        //            ActivityId = 3, Id = 9, FreePlaces = 5, Price = 1250, SlotDateTime = new DateTime(2023,11,20,18,30,0)
        //        },

        //        new Slot()
        //        {
        //            ActivityId = 4, Id = 10, FreePlaces = 5, Price = 1500, SlotDateTime = new DateTime(2023,11,20,16,30,0)
        //        },
        //        new Slot()
        //        {
        //            ActivityId = 4, Id = 11, FreePlaces = 5, Price = 2000, SlotDateTime = new DateTime(2023,11,20,10,30,0)
        //        },
        //        new Slot()
        //        {
        //            ActivityId = 4, Id = 12, FreePlaces = 5, Price = 1250, SlotDateTime = new DateTime(2023,11,20,18,30,0)
        //        },

        //        new Slot()
        //        {
        //            ActivityId = 5, Id = 13, FreePlaces = 5, Price = 1500, SlotDateTime = new DateTime(2023,11,20,16,30,0)
        //        },
        //        new Slot()
        //        {
        //            ActivityId = 5, Id = 14, FreePlaces = 5, Price = 2000, SlotDateTime = new DateTime(2023,11,20,10,30,0)
        //        },
        //        new Slot()
        //        {
        //            ActivityId = 5, Id = 15, FreePlaces = 5, Price = 1250, SlotDateTime = new DateTime(2023,11,20,18,30,0)
        //        },

        //        new Slot()
        //        {
        //            ActivityId = 6, Id = 16, FreePlaces = 5, Price = 1500, SlotDateTime = new DateTime(2023,11,20,16,30,0)
        //        },
        //        new Slot()
        //        {
        //            ActivityId = 6, Id = 17, FreePlaces = 5, Price = 2000, SlotDateTime = new DateTime(2023,11,20,10,30,0)
        //        },
        //        new Slot()
        //        {
        //            ActivityId = 6, Id = 18, FreePlaces = 5, Price = 1250, SlotDateTime = new DateTime(2023,11,20,18,30,0)
        //        },

        //        new Slot()
        //        {
        //            ActivityId = 7, Id = 19, FreePlaces = 5, Price = 1500, SlotDateTime = new DateTime(2023,11,20,16,30,0)
        //        },
        //        new Slot()
        //        {
        //            ActivityId = 7, Id = 20, FreePlaces = 5, Price = 2000, SlotDateTime = new DateTime(2023,11,20,10,30,0)
        //        },
        //        new Slot()
        //        {
        //            ActivityId = 7, Id = 21, FreePlaces = 5, Price = 1250, SlotDateTime = new DateTime(2023,11,20,18,30,0)
        //        },
        //    };
        private readonly ILogger<SlotController> _logger;

        public SlotController(ILogger<SlotController> logger, ISlotRepository slotRepository)
        {
            _logger = logger;
            this.slotRepository = slotRepository;
        }

        [HttpGet(Name = "GetSlotsByCategory")]
        public IActionResult GetSlotsByCategoryIdAndTime(int categoryId, DateTime dateTime)
        {
            var slots = slotRepository.GetSlotsByCategoryIdAndTime(categoryId, dateTime);
            List<GetSlotsByCategoryAndDateResponse> resp = slots.Select(x => new GetSlotsByCategoryAndDateResponse
            {
                SlotId = x.Id,
                ActivityName = x.Activity.ActivityName,
                CategoryName = x.Activity.Category.Name,
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
            }).ToList();

            return Ok(resp);
        }
        [HttpGet(Name = "GetSlotsByActivityIdAndTimeTest")]
        public IActionResult GetSlotsByActivityIdAndTimeTest(int activityId, DateTime dateTime)
        {
            return Ok(slotRepository.GetSlotsByActivityIdAndTime(activityId, dateTime));
        }
        [HttpGet(Name = "GetSlotTest")]
        public Slot GetSlot(int id)
        {
            return slotRepository.GetById(id);
        }
        [HttpGet(Name = "GetActivityViewSlots")]
        public IActionResult GetActivityViewSlots(int activityId, DateTime Date)
        {
            var slots = slotRepository.GetAllByActivityId(activityId).Where(x => x.SlotDateTime.Date == Date.Date);
            return Ok(slots.Select(x => 
                new SlotView { 
                    DurationMinutes = x.LengthMinutes, 
                    Price = x.Price, 
                    SlotId = x.Id, 
                    StartTime = 
                    x.SlotDateTime 
                }).ToArray());
        }
        [HttpGet(Name = "GetSlotFreeSpots")]
        public IActionResult GetSlotFreeSpots(int id)
        {
            var slot = slotRepository.GetById(id);
            if (slot == null)
            {
                return NotFound($"Slot with id {id} not found");
            }
            return Ok(slot.FreePlaces);
        }
    }
}