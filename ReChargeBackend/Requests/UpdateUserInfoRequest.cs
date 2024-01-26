namespace ReChargeBackend.Requests
{
    public class UpdateUserInfoRequest
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay {  get; set; }
        public string Gender { get; set; }
        public string ImageURL { get; set; }
        public string accessToken { get; set; }
    }
}
