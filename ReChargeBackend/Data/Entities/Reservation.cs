using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("reservation")]
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

        public Slot Slot { get; set; }
        public User User { get; set; }

    }
}
