using ReChargeBackend.Utility;

namespace ReChargeBackend.Responses
{
    public class GetNextReservationResponse
    {
        //todo: нужны name, imageUrl, locationName, addressString, coordinates, time
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateTime { get; set; }
        public Coordinates Coordinates { get; set; }
        public string LocationName { get; set; }
        public string AddressString { get; set; }
        public int ReservationId {  get; set; }

    }
}
