using Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReChargeBackend.Responses
{
    public class GetReservationResponse
    {
        public int SlotId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Count { get; set; }

    }
}
