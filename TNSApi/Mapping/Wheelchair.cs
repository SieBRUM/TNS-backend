using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TNSApi.Mapping.Link_tables;

namespace TNSApi.Mapping
{
    [Table("tbl_wheelchairs")]
    public class Wheelchair
    {
        [Key]
        [Column("WheelchairId")]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int UserId { get; set; }
        public int? AdditionId { get; set; }
        [Required]
        public int RalId { get; set; }

        [Required]
        public int SerialNumber { get; set; }

        [Required]
        [Column("DealerName")]
        public string Dealer { get; set; }
        public DateTime? OrderDate { get; set; }
        [Required]
        public DateTime DateOfMeasurement { get; set; }

        [Required]
        public double SeatWidth { get; set; }
        [Required]
        public double FootplateWidth { get; set; }
        [Required]
        public double SeatDepth { get; set; }
        [Required]
        public double FrameLength { get; set; }
        [Required]
        public double BackrestHeight { get; set; }
        [Required]
        public double SeatHeightFront { get; set; }
        [Required]
        public double SeatHeightBack { get; set; }
        [Required]
        public double BalancePoint { get; set; }
        [Required]
        public double LowerLegWidth { get; set; }

        [ForeignKey("AdditionId")]
        public virtual Addition Addition { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("RalId")]
        public virtual RalColor Color { get; set; }

        public ICollection<WheelchairArticle> Articles { get; set; }
        public ICollection<WheelchairFrontwheel> Frontwheels { get; set; }
        public ICollection<WheelchairHoop> Hoops { get; set; }
        public ICollection<WheelchairTire> Tires { get; set; }
        public ICollection<WheelchairWheelprotector> Wheelprotectors { get; set; }
        public ICollection<WheelchairWheel> Wheels { get; set; }
    }
}