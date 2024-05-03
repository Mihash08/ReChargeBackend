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
        public async Task<IActionResult> GetLocations()
        {
            return Ok(await locationRepository.GetAllAsync());
        }
        [HttpGet(Name = "GetLocation")]
        public async Task<IActionResult> GetLocation(int id)
        {
            
            return Ok(await locationRepository.GetByIdAsync(id));
        }
    }
}