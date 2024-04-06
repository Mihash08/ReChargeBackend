using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ReChargeBackend.Responses;

namespace BackendReCharge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityRepository activityRepository;
        private readonly ISlotRepository slotRepository;
        //List<Activity> acts = new List<Activity>()
        //    {
        //        new Activity() {
        //            ActivityAdminTg = "@Mihash08",
        //            ActivityAdminWa = "+79251851096",
        //            ActivityDescription = "Свободное плавание в бассейне Чайка это 5 дорожек длинной " +
        //            "50 метров с разной глубиной. Занимайтесь плаваньем для того-то чего-то и будет все хорошо",
        //            ActivityName = "Свободное плавание 45 минут",
        //            CategoryId = 4,
        //            Id = 1,
        //            LocationId = 1,
        //            ShouldDisplayWarning = true,
        //            WarningText = "Требуется справка"
        //        },
        //        new Activity() {
        //            ActivityAdminTg = "@Mihash08",
        //            ActivityAdminWa = "+79251851096",
        //            ActivityDescription = "Проход в Fitness Planet даёт вам безлимитный доступ ко всем тренажёрам, " +
        //            "раздевалке, сауне и душу, а так же бесплатным косультациям тренеров",
        //            ActivityName = "Проход в Fitness Planet",
        //            CategoryId = 3,
        //            Id = 2,
        //            LocationId = 2,
        //            ShouldDisplayWarning = false,
        //            WarningText = ""
        //        },
        //        new Activity() {
        //            ActivityDescription = "Тренировка с профессиональным тренером это намного эффективнее и круче! " +
        //            "А если вам кажется, что вы и сами все знаете, попробуйте тренировку с тренером в Fitness Planet и познайте, что такое" +
        //            "по настоящему интенсивная тренировка ",
        //            ActivityName = "Часовая тренировка с личным тренером",
        //            CategoryId = 3,
        //            Id = 3,
        //            LocationId = 2,
        //            ShouldDisplayWarning = false,
        //            WarningText = ""
        //        },
        //        new Activity() {
        //            ActivityDescription = "Час с тренером в клубе Ударник, это самый ценный час в вашей жизни. " +
        //            "Займитесь настоящим боксом с тренером, который подготавливал реальных профессионалов.",
        //            ActivityName = "Часовая тренировка с тренером",
        //            CategoryId = 1,
        //            Id = 4,
        //            LocationId = 3,
        //            ShouldDisplayWarning = false,
        //            WarningText = "Вас будут бить"
        //        },
        //        new Activity() {
        //            ActivityDescription = "3 часа в группе в клубе Ударник, это самые ценные 3 часа в вашей жизни. " +
        //            "Займитесь настоящим боксом с тренером, который подготавливал реальных профессионалов, и практикуйтесь вместе с группой.",
        //            ActivityName = "3-ех часовое занятие в группе",
        //            CategoryId = 1,
        //            Id = 5,
        //            LocationId = 3,
        //            ShouldDisplayWarning = false,
        //            WarningText = "Вас будут бить"
        //        },
        //        new Activity() {
        //            ActivityDescription = "Самые лучшие корты в Москве. Регулярно убираются и чистятся. Наши уборщики вообще не спят.",
        //            ActivityName = "Аренда корта на час",
        //            CategoryId = 2,
        //            Id = 6,
        //            LocationId = 4,
        //            ShouldDisplayWarning = true,
        //            WarningText = "Требуется своя экипировка"
        //        },
        //        new Activity() {
        //            ActivityDescription = "Тренер вам покажет как это делается. Вы ничего не умеете, а он умеет всё. Вы тупые, он гений.",
        //            ActivityName = "2-ух часовое занятие с тренером",
        //            CategoryId = 2,
        //            Id = 7,
        //            LocationId = 4,
        //            ShouldDisplayWarning = false,
        //            WarningText = "Требуется своя экипировка"
        //        },
        //    };
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
                    // Handle case when no activities are found
                    return NotFound();
                }

                var list = acts.Select(x => new GetActivitiesRecommendationsResponse
                {
                    Name = x.ActivityName ?? "Unknown",
                    AddressString = $"{x.Location?.AddressCity ?? ""} {x.Location?.AddressStreet ?? ""} {x.Location?.AddressBuildingNumber ?? ""}",
                    imageUrl = x.ImageUrl ?? "",
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
        [HttpGet(Name = "GetActivityTest")]
        public GetActivityIdResponse GetActivity(int id)
        {
            var act = activityRepository.GetById(id);
            if (act is null)
            {
                return new GetActivityIdResponse()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    StatusMessage = "Activity not found"
                };
            }
            return new GetActivityIdResponse()
            {
                StatusCode = StatusCodes.Status200OK,
                Activity = act,

            };
        }
        [HttpGet(Name = "GetNextActivity")]
        public IActionResult GetNextActivity(string accessToken)
        {
            //todo: this is mock
            try
            {
                var act = activityRepository.GetById(2);
                var response = new GetNextActivityResponse
                {
                    Name = act.ActivityName,
                    imageUrl = act.ImageUrl,
                    timeMilliseconds = act.Slots[0].SlotDateTime.ToUniversalTime().Millisecond,
                    AddressString = $"{act.Location?.AddressCity ?? ""} {act.Location?.AddressStreet ?? ""} {act.Location?.AddressBuildingNumber ?? ""}",
                    Coordinates = new Coordinates
                    {
                        Latitude = act.Location.AddressLatitude,
                        Longitude = act.Location.AddressLongitude
                    },
                    LocationName = act.Location.LocationName
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet(Name = "GetActivityView")]
        public GetActivityViewResponse GetActivityView(int id)
        {
            //todo: смотри фигму экран 6
            var act = activityRepository.GetById(id);
            if (act is null)
            {
                return new GetActivityViewResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    StatusMessage = "Activity not found"
                };
            }
            return new GetActivityViewResponse
            {
                StatusCode = StatusCodes.Status200OK,
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
                ActivityDescription = act.ActivityDescription
            };
        }
        [HttpGet(Name = "GetActivitiesByCategory")]
        public List<Activity> GetActivityByCategory(int categoryId)
        {
            var acts = activityRepository.GetByCategory(categoryId).ToList();
            return acts;
        }
    }
}