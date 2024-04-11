namespace ReChargeBackend.Responses
{
    public class GetUserByTokenResponse
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email {  get; set; }
        public DateTime? BirthDate {  get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public string? Gender { get; set; }
        public string? City { get; set; }
    }
}
