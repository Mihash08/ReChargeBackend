using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("activity")]
    public class Activity : BaseEntity
    {
        [Column("activity_name")]
        public string ActivityName { get; set; }
        [ForeignKey("Location")]
        [Column("location_id")]
        public int LocationId { get; set; }
        [ForeignKey("Category")]
        [Column("category_id")]
        public int CategoryId { get; set; }
        [Column("activity_description")]
        public string ActivityDescription { get; set; }
        [Column("activity_admin_tg")]
        public string? ActivityAdminTg { get; set; }
        [Column("activity_admin_wa")]
        public string? ActivityAdminWa { get; set; }
        [Column("warning_text")]
        public string? WarningText { get; set; }
        [Column("should_display_warning")]
        public bool ShouldDisplayWarning { get; set; }
        [Column("image_url")]
        public string? ImageUrl { get; set; }
        [Column("cancelation_message")]
        public string? CancelationMessage {  get; set; }
        public Location Location { get; set; }
        public Category Category { get; set; }

        public IList<Slot>? Slots { get; set; }

    }
}
