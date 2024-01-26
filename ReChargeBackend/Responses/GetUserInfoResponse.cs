using System;

namespace ReChargeBackend.Responses
{
    public class GetUserInfoResponse
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Gender { get; set; }

    }
}
