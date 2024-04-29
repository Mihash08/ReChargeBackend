using Data.Entities;

namespace ReChargeBackend.Responses
{
    public class GetLocationReservationsResponse
    {
        public int ReservationId { get; set; }
        public string ActivityName { get; set; }
        public DateTime SlotTime { get; set; }
        public int ReservationCount { get; set; }
        public int TotalPrice { get; set; } 
        public Status Status { get; set; }
    }
}
