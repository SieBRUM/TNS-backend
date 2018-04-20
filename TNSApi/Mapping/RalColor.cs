using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TNSApi.Mapping
{
    [Table("tbl_ralcolors")]
    public class RalColor
    {
        [Key]
        [Column("RalId")]
        public int Id { get; set; }

        [Required]
        [Column("RalColorCode")]
        public int ColorCode { get; set; }
    }
}