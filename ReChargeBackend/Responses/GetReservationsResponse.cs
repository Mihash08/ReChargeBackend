using Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReChargeBackend.Responses
{
    public class GetReservationsResponse
    {
        public DateTime SelectedDateTimeStart { get; set; }
        public DateTime SelectedDateTimeEnd { get; set; }
        public List<GetSingleReservation> Reservations { get; set; }

    }
    public class GetSingleReservation
    {
        public int ReservationId { get; set; }
        public string LocationName {  get; set; }
        public string? ImageUrl {  get; set; }
        public DateTime DateTime { get; set; }
        public string Address {  get; set; }
        public string ActivityName {  get; set; }
        public double CoordinatesLatitude { get; set; }
        public double CoordinatesLongitude { get; set; }
        public int ActivityId {  get; set; }

    }
}
