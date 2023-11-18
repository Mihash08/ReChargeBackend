using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendReCharge.Data.Entities
{
    [Table("user")]
    public class User : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("surname")]
        public string? Surname { get; set; }
        [Column("email")]
        public string? Email { get; set; }
        [Column("birthdate")]
        public DateTime? BirthDate { get; set; }
        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        public virtual IList<Reservation>? Reservations { get; set; }
        public User(string name, string? surname, string? email, DateTime? birthDate, string phoneNumber) : base()
        {
            Name = name;
            Surname = surname;
            Email = email;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
        }
        public User() : base()
        {
            Name = "";
            PhoneNumber = "";
        }
    }
}
