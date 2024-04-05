using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendReCharge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LocationController : ControllerBase
    {
        private ILocationRepository locationRepository;
        private readonly ILogger<LocationController> _logger;

        public LocationController(ILogger<LocationController> logger, ILocationRepository locationRepository)
        {
            _logger = logger;
            this.locationRepository = locationRepository;
        }

        [HttpGet(Name = "GetLocations")]
        public IEnumerable<Location> GetLocations()
        {
            return locationRepository.GetAll();
        }
        [HttpGet(Name = "GetLocation")]
        public Location GetLocation(int id)
        {
            
            return locationRepository.GetById(id);
        }
    }
}