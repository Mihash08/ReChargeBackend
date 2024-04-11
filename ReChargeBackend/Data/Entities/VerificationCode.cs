using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("verification_code")]
    public class VerificationCode : BaseEntity
    {
        [Column("phone_number")]
        public string PhoneNumber { get; set; }
        [Column("code")]
        public string Code { get; set; }
        [Column("session_id")]
        public string SessionId { get; set; }
        [Column("creation_datetime")]
        public DateTime CreationDateTime { get; set; }

    }
}
