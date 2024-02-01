namespace ReChargeBackend.Responses
{
    public class AuthResponse
    {
        public int statusCode {  get; set; }
        public string? statusMessage { get; set; }
        public string? accessToken { get; set; }
    }
}
