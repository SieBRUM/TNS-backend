using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TNSApi.Mapping
{
    [Table("tbl_tires")]
    public class Tire
    {
        [Key]
        [Column("TireId")]
        public int Id { get; set; }

        [Required]
        public double Price { get; set; }
        [Required]
        public string Description { get; set; }
    }
}