namespace ReChargeBackend.Responses
{
    public class AuthResponse
    {
        public bool isSuccess {  get; set; }
        public string? accessToken { get; set; }
        public string? message { get; set; }
    }
}
