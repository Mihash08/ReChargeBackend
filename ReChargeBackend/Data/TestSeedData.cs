using Microsoft.EntityFrameworkCore;
using ReCharge.Data;
using Data.Entities;
using Utility;
using ReChargeBackend.Utility;

namespace ReChargeBackend.Data
{
    public class TestSeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<AppDbContext>();
            context.Database.OpenConnection();

            Random rnd = new Random();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    AccessHash = Hasher.Encrypt("123"),
                    BirthDate = DateTime.Now,
                    Email = "mihsasandomirskiy@gmail.com",
                    Gender = "male",
                    Id = 1,
                    Name = "Mikhail",
                    PhoneNumber = "+79251851096",
                    Surname = "Sandomirskii",
                    City = "Москва",
                    ImageUrl = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png",
                });
            }

            //context.Database.ExecuteSql($"SET IDENTITY_INSERT dbo.location ON");
            if (!context.Locations.Any())
            {
                context.Locations.AddRange(
                new Location
                {
                    Id = 2,
                    AddressBuildingNumber = "1с5",
                    AddressCity = "Москва",
                    AddressLatitude = 55.824104,
                    AddressLongitude = 37.499872,
                    AddressNearestMetro = "Войковская",
                    AddressStreet = "Старопетровский проезд",
                    AdminTG = "Mihash08",
                    AdminWA = "+79251851096",
                    LocationDescription = "Спортивный зал Fitness Planet на Войковской это новейший спортивный зал с чем-то там еще новым," +
                    "типа новыми технологиями и добрыми тренерами по выгодной цене",
                    LocationName = "Fitness Planet"
                },
                new Location
                {
                    Id = 6,
                    AddressBuildingNumber = "24",
                    AddressCity = "Москва",
                    AddressLatitude = 56.024104,
                    AddressLongitude = 36.599872,
                    AddressNearestMetro = "Алексеевская",
                    AddressStreet = "Коптевский проезд",
                    AdminTG = "Mihash08",
                    AdminWA = "+79251851096",
                    LocationDescription = "Спортивный зал Spirit Fitness на Алексеевской это новейший спортивный зал с чем-то там еще новым," +
                    "типа новыми технологиями и добрыми тренерами по выгодной цене",
                    LocationName = "Spirit Fitness"
                },
                new Location
                {
                    Id = 7,
                    AddressBuildingNumber = "43",
                    AddressCity = "Москва",
                    AddressLatitude = 55.924104,
                    AddressLongitude = 36.799872,
                    AddressNearestMetro = "Бабушкинская",
                    AddressStreet = "Кривошеев переулок",
                    AdminTG = "Mihash08",
                    AdminWA = "+79251851096",
                    LocationDescription = "Спортивный зал Sport Now на Алексеевской это новейший спортивный зал с чем-то там еще новым," +
                    "типа новыми технологиями и добрыми тренерами по выгодной цене",
                    LocationName = "Sport Now"
                },
                new Location
                {
                    Id = 1,
                    AddressBuildingNumber = "39с53",
                    AddressCity = "Москва",
                    AddressLatitude = 55.837442,
                    AddressLongitude = 37.479109,
                    AddressNearestMetro = "Водный стадион",
                    AddressStreet = "Ленинградское шоссе",
                    AdminTG = "Mihash08",
                    AdminWA = "+79251851096",
                    LocationDescription = "Бассейн Динамо предлагает вам прийти к ним покупаться и узнать, что такое понастоящему чистая вода." +
                    " Настолько гениально продуманой раздевалки не видал народ никогда и вы просто офигеете, когда узнаете, что кладут в пирожки" +
                    " в местной столовке",
                    LocationName = "Бассейн Динамо"
                },
                new Location
                {
                    Id = 3,
                    AddressBuildingNumber = "16с60",
                    AddressCity = "Москва",
                    AddressLatitude = 55.805673,
                    AddressLongitude = 37.645428,
                    AddressNearestMetro = "Алексеевская",
                    AddressStreet = "3-я Мытищинская улица",
                    AdminTG = "Mihash08",
                    AdminWA = "+79251851096",
                    LocationDescription = "Новый зал единоборств клуба Ударник Алексеевская / ВДНХ это один из самых просторных залов нашего клуба." +
                    " Проводятся тренировки по Боксу, ММА, Детскому САМБО Кикбоксингу, Тайскому боксу, ФитБоксу, Карате. В" +
                    " зале установлен профессиональный боксерский ринг",
                    LocationName = "Боксерский клуб Ударник Алексеевская"
                },
                new Location
                {
                    Id = 4,
                    AddressBuildingNumber = "25Ас2",
                    AddressCity = "Москва",
                    AddressLatitude = 55.827385,
                    AddressLongitude = 37.479324,
                    AddressNearestMetro = "Алексеевская",
                    AddressStreet = "Ленинградское шоссе",
                    AdminTG = "Mihash08",
                    AdminWA = "+79251851096",
                    LocationDescription = "Супер тренировки, супер корты, самое новое оборудование и добрые тренера (или тренеры)." +
                    " Нет в мире лучше теннисного клуба, чем Tennis Capital",
                    LocationName = "Tennis Capital"
                },
                new Location
                {
                    Id = 5,
                    AddressBuildingNumber = "23",
                    AddressCity = "Москва",
                    AddressLatitude = 55.827385,
                    AddressLongitude = 37.479324,
                    AddressNearestMetro = "Алексеевская",
                    AddressStreet = "Ленинградское шоссе",
                    AdminTG = "Mihash08",
                    AdminWA = "+79251851096",
                    LocationDescription = "Ноготочки супер топ. Лучшие в мире. Еще массаж есть, просто отличный.",
                    LocationName = "Nails and Spa"
                }
                );
            }
            context.SaveChanges();
            //context.Database.ExecuteSql($"SET IDENTITY_INSERT dbo.location OFF");

            //context.Database.ExecuteSql($"SET IDENTITY_INSERT dbo.category ON");
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { 
                        Id = 4, 
                        Name = "Плаванье", 
                        Image = "https://cdn-icons-png.flaticon.com/512/50/50004.png", 
                        CategoryCategoryId = 0,
                        FullImageUrl = "https://www.health.com/thmb/Yv4HuoQyNbHNNxgtOTm63zqxurQ=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/Health-Swimming-080c78802f384a4687df5a3b13d5611e-3719a8e40a3c4c43a63a4d795e47c505.jpg"
                    },
                    new Category { 
                        Id = 3, 
                        Name = "Спорт залы", 
                        Image = "https://cdn-icons-png.flaticon.com/512/755/755298.png", 
                        CategoryCategoryId = 0,
                        FullImageUrl = "https://prod-ne-cdn-media.puregym.com/media/819394/gym-workout-plan-for-gaining-muscle_header.jpg?quality=80",
                    },
                    new Category { 
                        Id = 1, 
                        Name = "Бокс", 
                        Image = "https://cdn-icons-png.flaticon.com/512/73/73029.png", 
                        CategoryCategoryId = 0,
                        FullImageUrl = "https://media.newyorker.com/photos/653fe516ebe5be0a063b2116/4:3/w_2276,h_1707,c_limit/Sanneh-UFC-10-30-23.jpg"
                    },
                    new Category { 
                        Id = 2, 
                        Name = "Теннис", 
                        Image = "https://icons.iconarchive.com/icons/iconsmind/outline/512/Tennis-icon.png", 
                        CategoryCategoryId = 0,
                        FullImageUrl = "https://cdn.britannica.com/57/183257-050-0BA11B4B/Roger-Federer-2012.jpg"
                    },
                    new Category { 
                        Id = 5, 
                        Name = "Ноготички", 
                        Image = "https://cdn-icons-png.flaticon.com/512/80/80405.png", 
                        CategoryCategoryId = 1,
                        FullImageUrl = "https://laque-lounge.ru/wp-content/uploads/2023/02/img_6394555.jpeg"
                    },
                    new Category { 
                        Id = 6, 
                        Name = "Массаж", 
                        Image = "https://cdn-icons-png.flaticon.com/512/837/837377.png", 
                        CategoryCategoryId = 1,
                        FullImageUrl = "https://i0.wp.com/post.healthline.com/wp-content/uploads/2022/03/massage-on-back-1296-728-header.jpg?w=1155&h=1528"
                    }
                );
            }
            context.SaveChanges();
            //context.Database.ExecuteSql($"SET IDENTITY_INSERT dbo.category OFF");


            //context.Database.ExecuteSql($"SET IDENTITY_INSERT dbo.activity ON");
            if (!context.Activities.Any())
            {
                context.Activities.AddRange(
                    new Activity()
                    {
                        ActivityAdminTg = "@Mihash08",
                        ActivityAdminWa = "+79251851096",
                        ActivityDescription = "Свободное плавание в бассейне Чайка это 5 дорожек длинной " +
                    "50 метров с разной глубиной. Занимайтесь плаваньем для того-то чего-то и будет все хорошо. " +
                    "Открытый бассейн «Чайка», который работает круглый год. Тут вам и кафе, и лежаки, и " +
                    "знакомство с фридайвингом, и теплая водичка в холодное время года, и даже соляная " +
                    "пещера. Обратите внимание, что для посещения обязательна справка," +
                    " но для удобства вы можете получить ее прямо на территории бассейна," +
                    " если штатный врач будет на месте.",
                        ActivityName = "Свободное плавание",
                        CategoryId = 4,
                        Id = 1,
                        LocationId = 1,
                        ShouldDisplayWarning = true,
                        WarningText = "Требуется справка",
                        ImageUrl = "https://static.sobaka.ru/images/image/01/54/53/63/_normal.jpg?v=1647893937",
                        CancelationMessage = "Обратите внимание, отмена доступна не позже, чем за 12 часов",
                    },
                    new Activity()
                    {
                        ActivityAdminTg = "@Mihash08",
                        ActivityAdminWa = "+79251851096",
                        ActivityDescription = "Свободное плавание в бассейне Чайка это 5 дорожек длинной " +
                    "50 метров с разной глубиной. Занимайтесь плаваньем для того-то чего-то и будет все хорошо. " +
                    "Открытый бассейн «Чайка», который работает круглый год. Тут вам и кафе, и лежаки, и " +
                    "знакомство с фридайвингом, и теплая водичка в холодное время года, и даже соляная " +
                    "пещера. Обратите внимание, что для посещения обязательна справка," +
                    " но для удобства вы можете получить ее прямо на территории бассейна," +
                    " если штатный врач будет на месте.",
                        ActivityName = "Свободное плавание",
                        CategoryId = 4,
                        Id = 10,
                        LocationId = 1,
                        ShouldDisplayWarning = true,
                        WarningText = "Требуется справка",
                        ImageUrl = "https://d1s9j44aio5gjs.cloudfront.net/2016/07/The_Benefits_of_Swimming.jpg",
                        CancelationMessage = "Обратите внимание, отмена доступна не позже, чем за 12 часов",
                    },
                    new Activity()
                    {
                        ActivityAdminTg = "@Mihash08",
                        ActivityAdminWa = "+79251851096",
                        ActivityDescription = "Проход в Fitness Planet даёт вам безлимитный доступ ко всем тренажёрам, " +
                        "раздевалке, сауне и душу, а так же бесплатным косультациям тренеров. В фитнес-клубе есть " +
                        "вело- и кардиотренажеры, силовое оборудование, эллипсы, беговые дорожки." +
                        " Самых популярных тренажеров в зале несколько экземпляров, чтобы всегда " +
                        "были свободные.\r\nМы заботимся о том, чтобы наши тренажерные залы были " +
                        "не только функциональными, но и эстетически красивыми: большие зеркала, " +
                        "панорамные окна и стильный интерьер, в котором получаются красивые фотографии.\r\n",
                        ActivityName = "Проход в Fitness Planet",
                        CategoryId = 3,
                        Id = 2,
                        LocationId = 2,
                        ShouldDisplayWarning = false,
                        ImageUrl = "https://www.hussle.com/blog/wp-content/uploads/2020/12/Gym-structure-1080x675.png",
                        CancelationMessage = "Обратите внимание, отмена доступна не позже, чем за 12 часов",
                    },
                    new Activity()
                    {
                        ActivityDescription = "Занятие с личным треннером в Fitness Planet. В фитнес-клубе есть " +
                        "вело- и кардиотренажеры, силовое оборудование, эллипсы, беговые дорожки." +
                        " Самых популярных тренажеров в зале несколько экземпляров, чтобы всегда " +
                        "были свободные.\r\nМы заботимся о том, чтобы наши тренажерные залы были " +
                        "не только функциональными, но и эстетически красивыми: большие зеркала, " +
                        "панорамные окна и стильный интерьер, в котором получаются красивые фотографии.\r\n",
                        ActivityName = "Тренировка с личным тренером",
                        CategoryId = 3,
                        Id = 3,
                        LocationId = 2,
                        ShouldDisplayWarning = false,
                        ImageUrl = "https://www.gymnas.ru/wp-content/uploads/2018/04/47510994_l-1024x626.jpg",
                        CancelationMessage = "Обратите внимание, отмена доступна не позже, чем за 12 часов",
                    },
                    new Activity()
                    {
                        ActivityAdminTg = "@Mihash08",
                        ActivityAdminWa = "+79251851096",
                        ActivityDescription = "Проход в Spirit Fitness даёт вам безлимитный доступ ко всем тренажёрам, " +
                        "раздевалке, сауне и душу, а так же бесплатным косультациям тренеров. В фитнес-клубе есть " +
                        "вело- и кардиотренажеры, силовое оборудование, эллипсы, беговые дорожки." +
                        " Самых популярных тренажеров в зале несколько экземпляров, чтобы всегда " +
                        "были свободные.\r\nМы заботимся о том, чтобы наши тренажерные залы были " +
                        "не только функциональными, но и эстетически красивыми: большие зеркала, " +
                        "панорамные окна и стильный интерьер, в котором получаются красивые фотографии.\r\n",
                        ActivityName = "Проход в Spirit Fitness",
                        CategoryId = 3,
                        Id = 13,
                        LocationId = 6,
                        ShouldDisplayWarning = false,
                        ImageUrl = "https://sportleader.kz/images/cms/opt%20tr/468884-pfz4p2-187.jpg",
                        CancelationMessage = "Обратите внимание, отмена доступна не позже, чем за 12 часов",
                    },
                    new Activity()
                    {
                        ActivityDescription = "Занятие с личным треннером в Spirit Fitness. В фитнес-клубе есть " +
                        "вело- и кардиотренажеры, силовое оборудование, эллипсы, беговые дорожки." +
                        " Самых популярных тренажеров в зале несколько экземпляров, чтобы всегда " +
                        "были свободные.\r\nМы заботимся о том, чтобы наши тренажерные залы были " +
                        "не только функциональными, но и эстетически красивыми: большие зеркала, " +
                        "панорамные окна и стильный интерьер, в котором получаются красивые фотографии.\r\n",
                        ActivityName = "Тренировка с личным тренером",
                        CategoryId = 3,
                        Id = 14,
                        LocationId = 6,
                        ShouldDisplayWarning = false,
                        ImageUrl = "https://niidpo.ru/uplfile/news_image/Professiya-fitnes-trenera-plyusyi-minusyi-nyuansyi.jpg",
                        CancelationMessage = "Обратите внимание, отмена доступна не позже, чем за 12 часов",
                    }, new Activity()
                    {
                        ActivityAdminTg = "@Mihash08",
                        ActivityAdminWa = "+79251851096",
                        ActivityDescription = "Проход в Sport Now даёт вам безлимитный доступ ко всем тренажёрам, " +
                        "раздевалке, сауне и душу, а так же бесплатным косультациям тренеров. В фитнес-клубе есть " +
                        "вело- и кардиотренажеры, силовое оборудование, эллипсы, беговые дорожки." +
                        " Самых популярных тренажеров в зале несколько экземпляров, чтобы всегда " +
                        "были свободные.\r\nМы заботимся о том, чтобы наши тренажерные залы были " +
                        "не только функциональными, но и эстетически красивыми: большие зеркала, " +
                        "панорамные окна и стильный интерьер, в котором получаются красивые фотографии.\r\n",
                        ActivityName = "Проход в Sport Now",
                        CategoryId = 3,
                        Id = 15,
                        LocationId = 7,
                        ShouldDisplayWarning = false,
                        ImageUrl = "https://static.tildacdn.com/tild3663-3261-4730-b762-383831356634/IMG_9824.jpg",
                        CancelationMessage = "Обратите внимание, отмена доступна не позже, чем за 12 часов",
                    },
                    new Activity()
                    {
                        ActivityDescription = "Занятие с личным треннером в Sport Now. В фитнес-клубе есть " +
                        "вело- и кардиотренажеры, силовое оборудование, эллипсы, беговые дорожки." +
                        " Самых популярных тренажеров в зале несколько экземпляров, чтобы всегда " +
                        "были свободные.\r\nМы заботимся о том, чтобы наши тренажерные залы были " +
                        "не только функциональными, но и эстетически красивыми: большие зеркала, " +
                        "панорамные окна и стильный интерьер, в котором получаются красивые фотографии.\r\n",
                        ActivityName = "Тренировка с личным тренером",
                        CategoryId = 3,
                        Id = 16,
                        LocationId = 7,
                        ShouldDisplayWarning = false,
                        ImageUrl = "https://proftren.ru/assets/images/articles/4093/trainer-min.jpg",
                        CancelationMessage = "Обратите внимание, отмена доступна не позже, чем за 12 часов",
                    },
                    new Activity()
                    {
                        ActivityDescription = "Приглашаем МУЖЧИН и ДЕВУШЕК на тренировки по боксу " +
                        "(в спортзале или на улице)\r\n\r\nДля начинающих и имеющих опыт!\r\n\r\nС нами Вы:\r\n" +
                        "• освоите основные боксерские навыки;\r\n• избавитесь от лишнего веса и приведёте " +
                        "своё тело в порядок;\r\n• \"выпустите пар\" и получите эмоциональную разгрузку;\r\n" +
                        "• научитесь защищать себя в конфликтных ситуациях;\r\n• получите равномерную нагрузку " +
                        "на мышцы всего тела, что позволит Вам выглядеть более подтянутым;\r\n• сможете разобрать " +
                        "свой режим питания и получите нужные рекомендации.",
                        ActivityName = "Тренировка с тренером",
                        CategoryId = 1,
                        Id = 4,
                        LocationId = 3,
                        ShouldDisplayWarning = false,
                        ImageUrl = "https://www.novosti.rs/data/images/2024-04-27/477269_dusan-veletic2_fmob.jpg",
                        CancelationMessage = "Обратите внимание, отмена доступна не позже, чем за 12 часов",
                    },
                    new Activity()
                    {
                        ActivityDescription = "Приглашаем МУЖЧИН и ДЕВУШЕК на групповые тренировки по боксу " +
                        "(в спортзале или на улице)\r\n\r\nДля начинающих и имеющих опыт!\r\n\r\nС нами Вы:\r\n" +
                        "• освоите основные боксерские навыки;\r\n• избавитесь от лишнего веса и приведёте " +
                        "своё тело в порядок;\r\n• \"выпустите пар\" и получите эмоциональную разгрузку;\r\n" +
                        "• научитесь защищать себя в конфликтных ситуациях;\r\n• получите равномерную нагрузку " +
                        "на мышцы всего тела, что позволит Вам выглядеть более подтянутым;\r\n• сможете разобрать " +
                        "свой режим питания и получите нужные рекомендации.",
                        ActivityName = "Занятие в группе",
                        CategoryId = 1,
                        Id = 5,
                        LocationId = 3,
                        ShouldDisplayWarning = false,
                        ImageUrl = "https://www.проф-бокс.рф/uploads/01112.jpg",
                        CancelationMessage = "Обратите внимание, отмена доступна не позже, чем за 12 часов",
                    },
                    new Activity()
                    {
                        ActivityDescription = "Закрытый теннисный корт. Покрытие 9-ти слойный хард со " +
                        "смягчением 2020 года.\r\nБесплатная парковка, раздевалки, душ.. ОБОРУДОВАНИЕ В " +
                        "АРЕНДУ НЕ ПРЕДОСТАВЛЯЕТСЯ\r\nОсвещение - 400 люкс \r\nКОРТ ДЛЯ ФОТОСЕССИЙ, " +
                        "СЪЕМОК И МЕРОПРИЯТИЙ НЕ ПРЕДОСТАВЛЯЕТСЯ",
                        ActivityName = "Аренда корта",
                        CategoryId = 2,
                        Id = 6,
                        LocationId = 4,
                        ShouldDisplayWarning = true,
                        WarningText = "Требуется своя экипировка",
                        ImageUrl = "https://static.tildacdn.com/tild3362-3463-4463-a365-326636613234/tild6333-6163-4962-b.jpg",
                        CancelationMessage = "Обратите внимание, отмена доступна не позже, чем за 12 часов",
                    },
                    new Activity()
                    {
                        ActivityDescription = "Закрытый теннисный корт. Покрытие 9-ти слойный хард со " +
                        "смягчением 2020 года.\r\nБесплатная парковка, раздевалки, душ.. ОБОРУДОВАНИЕ В " +
                        "АРЕНДУ НЕ ПРЕДОСТАВЛЯЕТСЯ\r\nОсвещение - 400 люкс \r\nКОРТ ДЛЯ ФОТОСЕССИЙ, " +
                        "СЪЕМОК И МЕРОПРИЯТИЙ НЕ ПРЕДОСТАВЛЯЕТСЯ",
                        ActivityName = "Занятие с тренером",
                        CategoryId = 2,
                        Id = 7,
                        LocationId = 4,
                        ShouldDisplayWarning = true,
                        WarningText = "Требуется своя экипировка",
                        ImageUrl = "https://ncrdo.ru/upload/medialibrary/02d/02d934523a5b1bef5862e7e375bdcfd0.jpg",
                        CancelationMessage = "Обратите внимание, отмена доступна не позже, чем за 12 часов",
                    },
                    new Activity()
                    {
                        ActivityDescription = "Хотите, чтобы Ваши руки выглядели красиво и ухоженно? " +
                        "Чтобы на ногтях было красивое покрытие: прозрачный или однотонный лак, " +
                        "френч, различные рисунки – подходящие под настроение или грядущее событие? " +
                        "Чтобы ноготь не был утоплен в слое кутикулы, и она не образовывала " +
                        "заусенцы каждый раз, когда нужно поработать руками. Все эти проблемы " +
                        "решает маникюр в салоне.  \r\n\r\nДля Вас имеются все современные и трендовые цвета" +
                        " покрытий, актуальные аксессуары для дизайна на короткие и длинные ногти и современные " +
                        "технологии для осуществления любой Вашей задумки.",
                        ActivityName = "Маникюр",
                        CategoryId = 5,
                        Id = 8,
                        LocationId = 5,
                        ShouldDisplayWarning = false,
                        ImageUrl = "https://malinari.ru/wp-content/uploads/2016/12/malinari-nails-3.jpg",
                        CancelationMessage = "Обратите внимание, отмена доступна не позже, чем за 12 часов",
                    },
                    new Activity()
                    {
                        ActivityDescription = "Хотите, чтобы Ваши руки выглядели красиво и ухоженно? " +
                        "Чтобы на ногтях было красивое покрытие: прозрачный или однотонный лак, " +
                        "френч, различные рисунки – подходящие под настроение или грядущее событие? " +
                        "Чтобы ноготь не был утоплен в слое кутикулы, и она не образовывала " +
                        "заусенцы каждый раз, когда нужно поработать руками. Все эти проблемы " +
                        "решает маникюр в салоне.  \r\n\r\nДля Вас имеются все современные и трендовые цвета" +
                        " покрытий, актуальные аксессуары для дизайна на короткие и длинные ногти и современные " +
                        "технологии для осуществления любой Вашей задумки.",
                        ActivityName = "Маникюр люкс",
                        CategoryId = 5,
                        Id = 11,
                        LocationId = 5,
                        ShouldDisplayWarning = false,
                        ImageUrl = "https://news.store.rambler.ru/img/73f6ba25ab95c800b0a6b95bc34c1227?img-1-resize=width%3A1280%2Cheight%3A1280%2Cfit%3Acover&img-format=auto",
                        CancelationMessage = "Обратите внимание, отмена доступна не позже, чем за 12 часов",
                    },
                    new Activity()
                    {
                        ActivityDescription = "Feedback - сеть студий массажа в Москве.\r\n\r\nМы собрали " +
                        "команду сертифицированных и опытных мастеров, разработали эффективные программы," +
                        " проверенные на себе, обернули это в первоклассный сервис и теперь хотим размять" +
                        " всю страну.\r\n\r\nНаша главная цель - сделать массаж неотъемлемой " +
                        "частью жизни современного человека.",
                        ActivityName = "Массаж",
                        CategoryId = 6,
                        Id = 9,
                        LocationId = 5,
                        ShouldDisplayWarning = false,
                        ImageUrl = "https://s0.rbk.ru/v6_top_pics/media/img/4/31/756672878587314.webp",
                        CancelationMessage = "Обратите внимание, отмена доступна не позже, чем за 12 часов",
                    },
                    new Activity()
                    {
                        ActivityDescription = "Feedback - сеть студий массажа в Москве.\r\n\r\nМы собрали " +
                        "команду сертифицированных и опытных мастеров, разработали эффективные программы," +
                        " проверенные на себе, обернули это в первоклассный сервис и теперь хотим размять" +
                        " всю страну.\r\n\r\nНаша главная цель - сделать массаж неотъемлемой " +
                        "частью жизни современного человека.",
                        ActivityName = "Массаж лица",
                        CategoryId = 6,
                        Id = 12,
                        LocationId = 5,
                        ShouldDisplayWarning = false,
                        ImageUrl = "https://shkolamm.ru/wp-content/uploads/2018/11/plasticheskiy-massage-lica-1024x683.jpg",
                        CancelationMessage = "Обратите внимание, отмена доступна не позже, чем за 12 часов",
                    }
                );  

            }
            context.SaveChanges();
            //context.Database.ExecuteSql($"SET IDENTITY_INSERT dbo.activity OFF");
            
            if (!context.Slots.Any())
            {
                int slotId = 1;
                for (int activityId = 1; activityId <= 16; activityId++)
                {
                    for (int daySkip = 0;  daySkip <= 7; daySkip++)
                    {
                        for (int hourSkip = 0;  hourSkip <= 14; hourSkip++)
                        {
                            context.Slots.Add(
                                new Slot()
                                {
                                    ActivityId = activityId,
                                    Id = slotId,
                                    FreePlaces = rnd.Next(0, 10),
                                    Price = rnd.Next(4, 8) * 250,
                                    SlotDateTime = new DateTime(2024, DateTime.Now.Month, DateTime.Now.Day, 7, 0, 0).AddHours(hourSkip).AddDays(daySkip).AddMinutes(rnd.Next(-1, 1) * 30),
                                    LengthMinutes = rnd.Next(3, 6) * 15,
                                }
                            );
                            slotId++;
                        }
                    }
                }
            }
            context.SaveChanges();

            if (!context.Reservations.Any())
            {
                context.Reservations.AddRange(
                    new Reservation { 
                        Id = 999999, 
                        SlotId = 30, 
                        UserId = 1,
                        Count = 1, 
                        Email = "mihsasandomirskiy@gmail.com",
                        Name = "Misha", 
                        PhoneNumber = "+79251851096", 
                        AccessCode = Temp.GenerateAccessCode(),
                        Status = Status.Confirmed
                    },
                    new Reservation { 
                        Id = 999998, 
                        SlotId = 54, 
                        UserId = 1, 
                        Count = 3, 
                        Email = "mihsasandomirskiy@gmail.com", 
                        Name = "Misha", 
                        PhoneNumber = "+79251851096",
                        AccessCode = Temp.GenerateAccessCode(),
                        Status = Status.CanceledByAdmin
                    },
                    new Reservation { 
                        Id = 999997, 
                        SlotId = 73,
                        UserId = 1, 
                        Count = 1, 
                        Email = "gled@gmail.com", 
                        Name = "Zhora", 
                        PhoneNumber = "+79251851096",
                        AccessCode = Temp.GenerateAccessCode(),
                        Status = Status.New
                    }
                );
            }
            context.SaveChanges();

            if(!context.AdminUsers.Any())
            {
                context.AdminUsers.AddRange(
                    new AdminUser 
                    { 
                        AccessHash = Hasher.Encrypt("12345"), 
                        Email = "mihash08@yandex.com", 
                        Id = 1, 
                        LocationId = 1, 
                        Name = "Антон", 
                        Surname = "Чураков", 
                        PhoneNumber = "9951160000" 
                    }
                );
            }

            context.SaveChanges();
            context.Database.CloseConnection();
        }
    }
}
