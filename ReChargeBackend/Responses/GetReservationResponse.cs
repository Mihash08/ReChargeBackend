using Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReChargeBackend.Responses
{
    public class GetReservationResponse
    { /*
                 * data class ReservationDataModel(
                val reservationId: Int? = null,
                val activityId: Int? = null,
                val time: Date? = null,
                val userNumber: Int,
                val accessCode: String? = null
                )
                */
        public int SlotId { get; set; }
        public int ReservationId { get; set; }
        public int ActivityId { get; set; }
        public DateTime DateTime { get; set; }
        public int Count { get; set; }
        public string AccessCode { get; set; }
        public State State { get; set; }

    }
}
