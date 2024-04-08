
namespace ReChargeBackend.Responses
{
    public class PhoneAuthResponse
    {
        public string? SessionId { get; set; }
        public string? TitleText { get; set; }
        public int CodeSize { get; set; }
        public ConditionalInfoResponse? ConditionalInfo { get; set; }
    }
}
