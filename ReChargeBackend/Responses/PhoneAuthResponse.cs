
namespace ReChargeBackend.Responses
{
    public class PhoneAuthResponse
    {
        public bool isSuccess { get; set; }
        public string? errorText { get; set; }
        public string? sessionId { get; set; }
        public string? titleText { get; set; }
        public int codeSize { get; set; }
        public ConditionalInfoResponse? conditionalInfo { get; set; }
    }
}
