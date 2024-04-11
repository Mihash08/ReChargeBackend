using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Data.Entities
{
    [Table("reservation_table")]
    public class Reservation : BaseEntity
    {
        [ForeignKey("Slot")]
        [Column("slot_id")]
        public int SlotId { get; set; }
        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("is_over")]
        public bool IsOver { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("phone_number")]
        public string PhoneNumber {  get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("count")]
        public int Count { get; set; }

        public Slot Slot { get; set; }
        public User User { get; set; }

    }
}
