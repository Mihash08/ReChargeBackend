using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BackendReCharge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReservationsController : ControllerBase
    {
        List<Reservation> reservations = new()
        {
            new Reservation {Id = 1, IsOver = false, SlotId= 2, UserId = 1 },
            new Reservation {Id = 2, IsOver = true, SlotId= 1, UserId = 1 },
            new Reservation {Id = 3, IsOver = false, SlotId= 4, UserId = 1 },
        };
        private readonly ILogger<ReservationsController> _logger;

        public ReservationsController(ILogger<ReservationsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetReservationsByUserId")]
        public IEnumerable<Reservation> GetReservationsByUserId(int userId)
        {
            return reservations.Where(x => x.UserId == userId);
        }
        [HttpGet(Name = "GetReservation")]
        public Reservation GetSlot(int id)
        {
            return reservations.Where(x => x.Id == id).First();
        }
    }
}