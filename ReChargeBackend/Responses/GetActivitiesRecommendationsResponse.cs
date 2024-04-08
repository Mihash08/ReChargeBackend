namespace ReChargeBackend.Responses
{
    public class GetActivitiesRecommendationsResponse
    {
        //todo: нужны name, imageUrl, startPrice, locationName, addressString, coordinates
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int StartPrice { get; set; }
        public string LocationName { get; set; }
        public string AddressString { get; set; }
        public int Id { get; set; }

    }
}
