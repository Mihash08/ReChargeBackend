namespace ReChargeBackend.Responses
{
    public struct Coordinates
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
    public class GetNextReservationResponse
    {
        public int? ReservationId { get; set; } 
        public string? locationName { get; set; }
        public string? ActivityName { get; set;}
        public DateTime? Time { get; set; }
        public string? ImageUrl {  get; set; }
        public float? Price { get; set; }
        public string? Address {  get; set; }
        public Coordinates? coordinates { get; set; }
        public int StatusCode {  get; set; }
        public string? StatusMessage { get; set;}
    }
}
