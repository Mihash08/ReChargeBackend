using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ReChargeBackend.Responses;
using ReChargeBackend.Utility;

namespace BackendReCharge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityRepository activityRepository;
        private readonly ISlotRepository slotRepository;
        private readonly ILogger<ActivityController> _logger;

        public ActivityController(ILogger<ActivityController> logger, IActivityRepository activityRepository)
        {
            _logger = logger;
            this.activityRepository = activityRepository;
        }

        [HttpGet]
        public IActionResult GetActivitiesRecommendations(int category_id = -1)
        {
            try
            {
                var acts = activityRepository.GetByCategory(category_id);
                if (acts == null || !acts.Any())
                {
                    return Ok(new List<GetActivitiesRecommendationsResponse>());
                }

                var list = acts.Select(x => new GetActivitiesRecommendationsResponse
                {
                    Name = x.ActivityName ?? "Unknown",
                    AddressString = $"{x.Location?.AddressCity ?? ""} {x.Location?.AddressStreet ?? ""} {x.Location?.AddressBuildingNumber ?? ""}",
                    ImageUrl = x.ImageUrl ?? "",
                    LocationName = x.Location?.LocationName ?? "Unknown",
                    StartPrice = x.Slots != null && x.Slots.Any() ? x.Slots.Min(s => s.Price) : 0,
                    Id = x.Id
                });

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet(Name = "GetActivity")]
        public IActionResult GetActivity(int id)
        {
            var act = activityRepository.GetById(id);
            if (act is null)
            {
                NotFound($"Activity with id {id} not found");
            }
            return Ok(act);
        }
       
        [HttpGet(Name = "GetActivityView")]
        public IActionResult GetActivityView(int id)
        {
            var act = activityRepository.GetById(id);
            if (act is null)
            {
                return NotFound($"Activity with id {id} not found");
            }
            return Ok(new GetActivityViewResponse
            {
                ActivityId = act.Id,
                ActivityName = act.ActivityName,
                AdminPhoneWA = act.ActivityAdminWa,
                AdminTgUsername = act.ActivityAdminTg,
                CancellationMessage = act.CancelationMessage,
                Coordinates = new Coordinates
                {
                    Latitude = act.Location.AddressLatitude,
                    Longitude = act.Location.AddressLongitude
                },
                ImageURL = act.ImageUrl,
                LocationAddress = act.Location.AddressCity + ", " + act.Location.AddressStreet + ", " + act.Location.AddressBuildingNumber,
                ActivityDescription = act.ActivityDescription,
                LocationName = act.Location.LocationName
            });
        }
        [HttpGet(Name = "GetActivitiesByCategory")]
        public List<Activity> GetActivityByCategory(int categoryId)
        {
            var acts = activityRepository.GetByCategory(categoryId).ToList();
            return acts;
        }
    }
}