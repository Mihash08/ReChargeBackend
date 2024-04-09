using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    [Table("location_table")]
    public class Location : BaseEntity
    {
        [Column("location_name")]
        public string LocationName { get; set; }
        [Column("admin_ig")]
        public string AdminTG { get; set; }
        [Column("admin_wa")]
        public string AdminWA { get; set; }
        [Column("image")]
        public string? Image { get; set; }

        [Column("location_description")]
        public string LocationDescription { get; set; }

        [Column("address_city")]
        public string AddressCity { get; set; }

        [Column("address_street")]
        public string AddressStreet { get; set; }

        [Column("address_building_number")]
        public string AddressBuildingNumber { get; set; }

        [Column("address_nearest_metro")]
        public string AddressNearestMetro { get; set; }

        [Column("address_longitude")]
        public double AddressLongitude { get; set; }
        [Column("address_latitude")]
        public double AddressLatitude { get; set; }

    }
}
