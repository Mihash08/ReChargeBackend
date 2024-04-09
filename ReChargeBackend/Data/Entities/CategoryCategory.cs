using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("category_category")]
    public class CategoryCategory : BaseEntity
    {
        //todo: add recomendationImage
        [Column("name")]
        public string Name { get; set; }
    }
}
