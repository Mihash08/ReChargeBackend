using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendReCharge.Data.Entities
{
    [Table("category")]
    public class Category : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("image")]
        public string? Image { get; set; }

        //TODO: Add products list
        public Category(string name) : base()
        {
            Name = name;
        }
    }
}
