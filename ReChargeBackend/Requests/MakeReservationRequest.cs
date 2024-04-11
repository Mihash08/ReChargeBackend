namespace ReChargeBackend.Requests
{
    public class MakeReservationRequest
    {
        public string Name {  get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int ReserveCount {  get; set; }
    }
}
