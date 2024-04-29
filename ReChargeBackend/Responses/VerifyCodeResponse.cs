namespace ReChargeBackend.Responses
{
    public class VerifyCodeResponse
    {
        public int ReservationId { get; set; }
        public DateTime ReservationStartTime {  get; set; }
        public int DurationInMinutes { get; set; }
        public String ActivityName {  get; set; }
        public String Username {  get; set; }
        public int NumberOfUsers {  get; set; }
        public int TotalPrice { get; set; }
    }
}
