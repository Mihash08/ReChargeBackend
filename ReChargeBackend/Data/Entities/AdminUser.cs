using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("admin_users")]
    public class AdminUser : BaseEntity
    {
        [Column("name")]
        public string? Name { get; set; }
        [Column("surname")]
        public string? Surname { get; set; }
        [Column("email")]
        public string? Email { get; set; }
        [Column("phone_number")]
        public string PhoneNumber { get; set; }
        [Column("access_hash")]
        public string? AccessHash {  get; set; }
        [ForeignKey("Location")]
        [Column("location_id")]
        public int LocationId {  get; set; }
        [Column("firebase_token")]
        public string? FirebaseToken { get; set; }
        public Location Location { get; set; }
        public AdminUser(string name, string? surname, string? email, DateTime? birthDate, string phoneNumber, string? imageUrl, string? accessHash, string? gender) : base()
        {
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phoneNumber;
            AccessHash = accessHash;
        }
        public AdminUser() : base()
        {
            Name = "";
            PhoneNumber = "";
        }
    }
}
