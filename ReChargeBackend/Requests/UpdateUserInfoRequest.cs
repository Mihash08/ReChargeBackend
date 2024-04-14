namespace ReChargeBackend.Requests
{
    public class UpdateUserInfoRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay {  get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
    }
}
