namespace ReChargeBackend.Responses
{
    public class GetSlotFreeSpotsResponse
    {
        public int? FreeSpots { get; set; }
        public int StatusCode { get; set; }
        public string? StatusMessage { get; set; }
    }
}
