
namespace ReChargeBackend.Responses
{
    public class GetActivityViewResponse
    {
        public int? ActivityId { get; set; }
        public string? ActivityName { get; set; }
        public string? ImageURL { get; set; }
        public string? AdminPhoneWA { get; set; }
        public string? AdminTgUsername { get; set; }
        public string? LocationName { get; set; }
        public string? LocationAddress { get; set; }
        public string? ActivityDescription { get; set; }
        public string? Warning { get; set; }
        public string? CancellationMessage { get; set; }
        public Coordinates? Coordinates { get; set; }
        public int StatusCode {  get; set; }
        public string? StatusMessage { get; set; }

    }
}