using System.Runtime.CompilerServices;

namespace ReChargeBackend.Responses
{
    public class GetProfileHeaderResponse
    {
        public string? Name { get; set; }
        public string? PhotoUrl { get; set; }
        public int StatusCode {  get; set; }
        public string? StatusMessage { get; set;}
    }
}
