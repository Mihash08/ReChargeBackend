namespace ReChargeBackend.Responses
{
    public class GetNextActivityResponse
    {
        //todo: нужны name, imageUrl, locationName, addressString, coordinates, time
        public string Name { get; set; }
        public string imageUrl { get; set; }
        public long timeMilliseconds { get; set; }
        public Coordinates Coordinates { get; set; }
        public string LocationName { get; set; }
        public string AddressString { get; set; }

    }
}
