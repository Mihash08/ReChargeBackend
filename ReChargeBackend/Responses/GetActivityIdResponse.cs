using Data.Entities;

namespace ReChargeBackend.Responses
{
    public class GetActivityIdResponse
    {
        public Activity? Activity { get; set; }
        public int StatusCode { get; set; }
        public string? StatusMessage {  get; set; }
    }
}
