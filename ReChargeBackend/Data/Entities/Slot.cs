using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("slot")]
    public class Slot : BaseEntity
    {
        [ForeignKey("Activity")]
        [Column("activity_id")]
        public int ActivityId { get; set; }

        [Column("date_time")]
        public DateTime SlotDateTime { get; set; }
        [Column("print")]
        public int Price { get; set; }

        [Column("free_places")]
        public int FreePlaces { get; set; }

        public Activity Activity { get; set; }

    }
}
