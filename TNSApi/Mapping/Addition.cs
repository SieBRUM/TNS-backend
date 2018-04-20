using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TNSApi.Mapping
{
    [Table("tbl_additions")]
    public class Addition
    {
        [Key]
        [Column("AdditionId")]
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Discount { get; set; }
        public int Amount { get; set; }
    }
}