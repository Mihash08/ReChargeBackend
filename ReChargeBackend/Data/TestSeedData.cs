using Microsoft.EntityFrameworkCore;
using ReCharge.Data;
using Data.Entities;
using Utility;

namespace ReChargeBackend.Data
{
    public class TestSeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<AppDbContext>();
            context.Database.OpenConnection();

            if (context.Database.GetPendingMigrations().Any())
            {
                //context.Database.Migrate();
            }

            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    AccessHash = Hasher.Encrypt("123"),
                    BirthDate = DateTime.Now,
                    Email = "mihsasandomirskiy@gmail.com",
                    Gender = "male",
                    Id = 0,
                    Name = "Mikhail",
                    PhoneNumber = "+79251851096",
                    Surname = "Sandomirskii",
                    ImageUrl = "https://images.ctfassets.net/h6goo9gw1hh6/2sNZtFAWOdP1lmQ33VwRN3/24e953b920a9cd0ff2e1d587742a2472/1-intro-photo-final.jpg?w=1200&h=992&fl=progressive&q=70&fm=jpg"
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
                }
                );
            }
            context.SaveChanges();
            //context.Database.ExecuteSql($"SET IDENTITY_INSERT dbo.location OFF");


            //context.Database.ExecuteSql($"SET IDENTITY_INSERT dbo.category ON");
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Id = 4, Name = "Плаванье", Image = "data:image/png;base64,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" },
                    new Category { Id = 3, Name = "Спорт залы", Image = "https://t4.ftcdn.net/jpg/03/29/67/97/360_F_329679742_4vrHnqpRSsqiTrLWEsmpLwvwHc3aNc4I.jpg" },
                    new Category { Id = 1, Name = "Бокс", Image = "https://static.vecteezy.com/system/resources/thumbnails/000/421/048/small/Sports__28112_29.jpg" },
                    new Category { Id = 2, Name = "Теннис", Image = "https://icons.iconarchive.com/icons/iconsmind/outline/512/Tennis-icon.png" }
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
                    "50 метров с разной глубиной. Занимайтесь плаваньем для того-то чего-то и будет все хорошо",
                        ActivityName = "Свободное плавание 45 минут",
                        CategoryId = 4,
                        Id = 1,
                        LocationId = 1,
                        ShouldDisplayWarning = true,
                        WarningText = "Требуется справка",
                        ImageUrl = "https://img.freepik.com/free-photo/sports-tools_53876-138077.jpg"
                    },
                    new Activity()
                    {
                        ActivityAdminTg = "@Mihash08",
                        ActivityAdminWa = "+79251851096",
                        ActivityDescription = "Проход в Fitness Planet даёт вам безлимитный доступ ко всем тренажёрам, " +
                        "раздевалке, сауне и душу, а так же бесплатным косультациям тренеров",
                        ActivityName = "Проход в Fitness Planet",
                        CategoryId = 3,
                        Id = 2,
                        LocationId = 2,
                        ShouldDisplayWarning = false,
                        WarningText = "",
                        ImageUrl = "https://img.freepik.com/free-photo/sports-tools_53876-138077.jpg"
                    },
                    new Activity()
                    {
                        ActivityDescription = "Тренировка с профессиональным тренером это намного эффективнее и круче! " +
                        "А если вам кажется, что вы и сами все знаете, попробуйте тренировку с тренером в Fitness Planet и познайте, что такое" +
                        "по настоящему интенсивная тренировка ",
                        ActivityName = "Часовая тренировка с личным тренером",
                        CategoryId = 3,
                        Id = 3,
                        LocationId = 2,
                        ShouldDisplayWarning = false,
                        WarningText = "",
                        ImageUrl = "https://img.freepik.com/free-photo/sports-tools_53876-138077.jpg"
                    },
                    new Activity()
                    {
                        ActivityDescription = "Час с тренером в клубе Ударник, это самый ценный час в вашей жизни. " +
                        "Займитесь настоящим боксом с тренером, который подготавливал реальных профессионалов.",
                        ActivityName = "Часовая тренировка с тренером",
                        CategoryId = 1,
                        Id = 4,
                        LocationId = 3,
                        ShouldDisplayWarning = false,
                        WarningText = "Вас будут бить",
                        ImageUrl = "https://img.freepik.com/free-photo/sports-tools_53876-138077.jpg"
                    },
                    new Activity()
                    {
                        ActivityDescription = "3 часа в группе в клубе Ударник, это самые ценные 3 часа в вашей жизни. " +
                        "Займитесь настоящим боксом с тренером, который подготавливал реальных профессионалов, и практикуйтесь вместе с группой.",
                        ActivityName = "3-ех часовое занятие в группе",
                        CategoryId = 1,
                        Id = 5,
                        LocationId = 3,
                        ShouldDisplayWarning = false,
                        WarningText = "Вас будут бить",
                        ImageUrl = "https://img.freepik.com/free-photo/sports-tools_53876-138077.jpg"
                    },
                    new Activity()
                    {
                        ActivityDescription = "Самые лучшие корты в Москве. Регулярно убираются и чистятся. Наши уборщики вообще не спят.",
                        ActivityName = "Аренда корта на час",
                        CategoryId = 2,
                        Id = 6,
                        LocationId = 4,
                        ShouldDisplayWarning = true,
                        WarningText = "Требуется своя экипировка",
                        ImageUrl = "https://img.freepik.com/free-photo/sports-tools_53876-138077.jpg"
                    },
                    new Activity()
                    {
                        ActivityDescription = "Тренер вам покажет как это делается. Вы ничего не умеете, а он умеет всё. Вы тупые, он гений.",
                        ActivityName = "2-ух часовое занятие с тренером",
                        CategoryId = 2,
                        Id = 7,
                        LocationId = 4,
                        ShouldDisplayWarning = false,
                        WarningText = "Требуется своя экипировка",
                        ImageUrl = "https://img.freepik.com/free-photo/sports-tools_53876-138077.jpg"
                    }
                );

            }
            context.SaveChanges();
            //context.Database.ExecuteSql($"SET IDENTITY_INSERT dbo.activity OFF");
            
            if (!context.Slots.Any())
            {
                context.Slots.AddRange(
                      new Slot()
                      {
                          ActivityId = 1,
                          Id = 1,
                          FreePlaces = 5,
                          Price = 1500,
                          SlotDateTime = new DateTime(2023, 11, 20, 16, 30, 0)
                      },
                new Slot()
                {
                    ActivityId = 1,
                    Id = 2,
                    FreePlaces = 5,
                    Price = 2000,
                    SlotDateTime = new DateTime(2023, 11, 20, 10, 30, 0),
                    LengthMinutes = 45,
                },
                new Slot()
                {
                    ActivityId = 1,
                    Id = 3,
                    FreePlaces = 5,
                    Price = 1250,
                    SlotDateTime = new DateTime(2023, 11, 20, 18, 30, 0),
                    LengthMinutes = 45,
                },

                        new Slot()
                        {
                            ActivityId = 2,
                            Id = 4,
                            FreePlaces = 5,
                            Price = 1500,
                            SlotDateTime = new DateTime(2023, 11, 20, 16, 30, 0),
                    LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 2,
                            Id = 5,
                            FreePlaces = 5,
                            Price = 2000,
                            SlotDateTime = new DateTime(2023, 11, 20, 10, 30, 0),
                    LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 2,
                            Id = 6,
                            FreePlaces = 5,
                            Price = 1250,
                            SlotDateTime = new DateTime(2023, 11, 20, 18, 30, 0),
                    LengthMinutes = 45,
                        },

                        new Slot()
                        {
                            ActivityId = 3,
                            Id = 7,
                            FreePlaces = 5,
                            Price = 1500,
                            SlotDateTime = new DateTime(2023, 11, 20, 16, 30, 0),
                            LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 3,
                            Id = 8,
                            FreePlaces = 5,
                            Price = 2000,
                            SlotDateTime = new DateTime(2023, 11, 20, 10, 30, 0),
                            LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 3,
                            Id = 9,
                            FreePlaces = 5,
                            Price = 1250,
                            SlotDateTime = new DateTime(2023, 11, 20, 18, 30, 0),
                            LengthMinutes = 45,
                        },

                        new Slot()
                        {
                            ActivityId = 4,
                            Id = 10,
                            FreePlaces = 5,
                            Price = 1500,
                            SlotDateTime = new DateTime(2023, 11, 20, 16, 30, 0),
                            LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 4,
                            Id = 11,
                            FreePlaces = 5,
                            Price = 2000,
                            SlotDateTime = new DateTime(2023, 11, 20, 10, 30, 0),
                            LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 4,
                            Id = 12,
                            FreePlaces = 5,
                            Price = 1250,
                            SlotDateTime = new DateTime(2023, 11, 20, 18, 30, 0),
                            LengthMinutes = 45,
                        },

                        new Slot()
                        {
                            ActivityId = 5,
                            Id = 13,
                            FreePlaces = 5,
                            Price = 1500,
                            SlotDateTime = new DateTime(2023, 11, 20, 16, 30, 0),
                            LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 5,
                            Id = 14,
                            FreePlaces = 5,
                            Price = 2000,
                            SlotDateTime = new DateTime(2023, 11, 20, 10, 30, 0),
                            LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 5,
                            Id = 15,
                            FreePlaces = 5,
                            Price = 1250,
                            SlotDateTime = new DateTime(2023, 11, 20, 18, 30, 0),
                            LengthMinutes = 45,
                        },

                        new Slot()
                        {
                            ActivityId = 6,
                            Id = 16,
                            FreePlaces = 5,
                            Price = 1500,
                            SlotDateTime = new DateTime(2023, 11, 20, 16, 30, 0),
                            LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 6,
                            Id = 17,
                            FreePlaces = 5,
                            Price = 2000,
                            SlotDateTime = new DateTime(2023, 11, 20, 10, 30, 0),
                            LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 6,
                            Id = 18,
                            FreePlaces = 5,
                            Price = 1250,
                            SlotDateTime = new DateTime(2023, 11, 20, 18, 30, 0),
                            LengthMinutes = 45,
                        },

                        new Slot()
                        {
                            ActivityId = 7,
                            Id = 19,
                            FreePlaces = 5,
                            Price = 1500,
                            SlotDateTime = new DateTime(2023, 11, 20, 16, 30, 0),
                            LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 7,
                            Id = 20,
                            FreePlaces = 5,
                            Price = 2000,
                            SlotDateTime = new DateTime(2023, 11, 20, 10, 30, 0),
                            LengthMinutes = 45,
                        },
                        new Slot()
                        {
                            ActivityId = 7,
                            Id = 21,
                            FreePlaces = 5,
                            Price = 1250,
                            SlotDateTime = new DateTime(2023, 11, 20, 18, 30, 0),
                            LengthMinutes = 45,
                        }
                    );
            }
            context.SaveChanges();

            if (!context.Reservations.Any())
            {
                context.Reservations.AddRange(
                    new Reservation { Id = 1, IsOver = false, SlotId = 2, UserId = 1 },
                    new Reservation { Id = 2, IsOver = true, SlotId = 1, UserId = 1 },
                    new Reservation { Id = 3, IsOver = false, SlotId = 4, UserId = 1 }
                );
            }
            context.SaveChanges();

            context.Database.CloseConnection();
        }
    }
}
