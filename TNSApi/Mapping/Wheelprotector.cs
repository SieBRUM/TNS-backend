using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TNSApi.Mapping
{
    [Table("tbl_wheelprotectors")]
    public class Wheelprotector
    {
        [Key]
        [Column("WheelProtectorId")]
        public int Id { get; set; }

        public double Price { get; set; }
        public string Description { get; set; }
    }
}