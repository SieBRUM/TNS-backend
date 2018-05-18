using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TNSApi.Mapping.Link_tables
{
    [Table("tbl_wheelchairs_frontwheels")]
    public class WheelchairFrontwheel
    {
        [Key]
        [Column("WheelchairFrontwheelId")]
        public int Id { get; set; }

        [Required]
        public int WheelchairId { get; set; }
        [Required]
        public int FrontWheelId { get; set; }
        public int? AdditionId { get; set; }

        [ForeignKey("FrontWheelId")]
        public virtual Frontwheel Frontwheel { get; set; }
        [ForeignKey("AdditionId")]
        public virtual Addition Addition { get; set; }
    }
}