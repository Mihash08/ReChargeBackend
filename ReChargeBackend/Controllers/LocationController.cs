using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BackendReCharge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LocationController : ControllerBase
    {
        List<Location> loc = new List<Location>()
            {
                new Location { Id = 2, AddressBuildingNumber = "1с5", AddressCity = "Москва",
                    AddressLatitude = 55.824104, AddressLongitude =  37.499872, AddressNearestMetro = "Войковская",
                AddressStreet = "Старопетровский проезд", AdminTG = "Mihash08", AdminWA = "+79251851096",
                    LocationDescription = "Спортивный зал Fitness Planet на Войковской это новейший спортивный зал с чем-то там еще новым," +
                    "типа новыми технологиями и добрыми тренерами по выгодной цене", LocationName = "Fitness Planet" },
                new Location { Id = 1, AddressBuildingNumber = "39с53", AddressCity = "Москва",
                    AddressLatitude = 55.837442, AddressLongitude =  37.479109, AddressNearestMetro = "Водный стадион",
                AddressStreet = "Ленинградское шоссе", AdminTG = "Mihash08", AdminWA = "+79251851096",
                    LocationDescription = "Бассейн Динамо предлагает вам прийти к ним покупаться и узнать, что такое понастоящему чистая вода." +
                    " Настолько гениально продуманой раздевалки не видал народ никогда и вы просто офигеете, когда узнаете, что кладут в пирожки" +
                    " в местной столовке", LocationName = "Бассейн Динамо"},
                new Location { Id = 3, AddressBuildingNumber = "16с60", AddressCity = "Москва",
                    AddressLatitude = 55.805673, AddressLongitude =  37.645428, AddressNearestMetro = "Алексеевская",
                AddressStreet = "3-я Мытищинская улица", AdminTG = "Mihash08", AdminWA = "+79251851096",
                    LocationDescription = "Новый зал единоборств клуба Ударник Алексеевская / ВДНХ это один из самых просторных залов нашего клуба." +
                    " Проводятся тренировки по Боксу, ММА, Детскому САМБО Кикбоксингу, Тайскому боксу, ФитБоксу, Карате. В" +
                    " зале установлен профессиональный боксерский ринг", LocationName = "Боксерский клуб Ударник Алексеевская"},
                new Location { Id = 4, AddressBuildingNumber = "25Ас2", AddressCity = "Москва",
                    AddressLatitude = 55.827385, AddressLongitude =  37.479324, AddressNearestMetro = "Алексеевская",
                AddressStreet = "Ленинградское шоссе", AdminTG = "Mihash08", AdminWA = "+79251851096",
                    LocationDescription = "Супер тренировки, супер корты, самое новое оборудование и добрые тренера (или тренеры)." +
                    " Нет в мире лучше теннисного клуба, чем Tennis Capital", LocationName = "Tennis Capital"}
            };
        private readonly ILogger<LocationController> _logger;

        public LocationController(ILogger<LocationController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetLocations")]
        public IEnumerable<Location> GetLocations()
        {
            return loc;
        }
        [HttpGet(Name = "GetLocation")]
        public Location GetLocation(int id)
        {
            
            return loc.Where(x => x.Id == id).First();
        }
    }
}