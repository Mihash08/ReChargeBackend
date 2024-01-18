
namespace ReChargeBackend.Requests
{
    public class AuthRequest
    {
        public string phoneNumber { get; set; }
        public string sessionId { get; set; }
        public string code { get; set; }

    }
}