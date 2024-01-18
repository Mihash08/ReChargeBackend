﻿using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BackendReCharge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ActivityController : ControllerBase
    {
        List<Activity> acts = new List<Activity>()
            {
                new Activity() {
                    ActivityAdminTg = "@Mihash08",
                    ActivityAdminWa = "+79251851096",
                    ActivityDescription = "Свободное плавание в бассейне Чайка это 5 дорожек длинной " +
                    "50 метров с разной глубиной. Занимайтесь плаваньем для того-то чего-то и будет все хорошо",
                    ActivityName = "Свободное плавание 45 минут",
                    CategoryId = 4,
                    Id = 1,
                    LocationId = 1,
                    ShouldDisplayWarning = true,
                    WarningText = "Требуется справка"
                },
                new Activity() {
                    ActivityAdminTg = "@Mihash08",
                    ActivityAdminWa = "+79251851096",
                    ActivityDescription = "Проход в Fitness Planet даёт вам безлимитный доступ ко всем тренажёрам, " +
                    "раздевалке, сауне и душу, а так же бесплатным косультациям тренеров",
                    ActivityName = "Проход в Fitness Planet",
                    CategoryId = 3,
                    Id = 2,
                    LocationId = 2,
                    ShouldDisplayWarning = false,
                    WarningText = ""
                },
                new Activity() {
                    ActivityDescription = "Тренировка с профессиональным тренером это намного эффективнее и круче! " +
                    "А если вам кажется, что вы и сами все знаете, попробуйте тренировку с тренером в Fitness Planet и познайте, что такое" +
                    "по настоящему интенсивная тренировка ",
                    ActivityName = "Часовая тренировка с личным тренером",
                    CategoryId = 3,
                    Id = 3,
                    LocationId = 2,
                    ShouldDisplayWarning = false,
                    WarningText = ""
                },
                new Activity() {
                    ActivityDescription = "Час с тренером в клубе Ударник, это самый ценный час в вашей жизни. " +
                    "Займитесь настоящим боксом с тренером, который подготавливал реальных профессионалов.",
                    ActivityName = "Часовая тренировка с тренером",
                    CategoryId = 1,
                    Id = 4,
                    LocationId = 3,
                    ShouldDisplayWarning = false,
                    WarningText = "Вас будут бить"
                },
                new Activity() {
                    ActivityDescription = "3 часа в группе в клубе Ударник, это самые ценные 3 часа в вашей жизни. " +
                    "Займитесь настоящим боксом с тренером, который подготавливал реальных профессионалов, и практикуйтесь вместе с группой.",
                    ActivityName = "3-ех часовое занятие в группе",
                    CategoryId = 1,
                    Id = 5,
                    LocationId = 3,
                    ShouldDisplayWarning = false,
                    WarningText = "Вас будут бить"
                },
                new Activity() {
                    ActivityDescription = "Самые лучшие корты в Москве. Регулярно убираются и чистятся. Наши уборщики вообще не спят.",
                    ActivityName = "Аренда корта на час",
                    CategoryId = 2,
                    Id = 6,
                    LocationId = 4,
                    ShouldDisplayWarning = true,
                    WarningText = "Требуется своя экипировка"
                },
                new Activity() {
                    ActivityDescription = "Тренер вам покажет как это делается. Вы ничего не умеете, а он умеет всё. Вы тупые, он гений.",
                    ActivityName = "2-ух часовое занятие с тренером",
                    CategoryId = 2,
                    Id = 7,
                    LocationId = 4,
                    ShouldDisplayWarning = false,
                    WarningText = "Требуется своя экипировка"
                },
            };
        private readonly ILogger<ActivityController> _logger;

        public ActivityController(ILogger<ActivityController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetActivities")]
        public IEnumerable<Activity> GetActivities()
        {
            return acts;
        }
        [HttpGet(Name = "GetActivity")]
        public Activity GetActivity(int id)
        {

            return acts.Where(x => x.Id == id).First();
        }
        [HttpGet(Name = "GetActivitiesByCategory")]
        public Activity GetActivityByCategory(int categoryId)
        {

            return acts.Where(x => x.CategoryId == categoryId).First();
        }
    }
}