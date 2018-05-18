using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TNSApi.Mapping.Link_tables
{
    [Table("tbl_wheelchairs_wheels")]
    public class WheelchairWheel
    {
        [Key]
        [Column("WheelchairWheelId")]
        public int Id { get; set; }

        [Required]
        public int WheelchairId { get; set; }
        [Required]
        public int WheelId { get; set; }
        public int? AdditionId { get; set; }

        [ForeignKey("WheelId")]
        public virtual Wheel Wheel { get; set; }
        [ForeignKey("AdditionId")]
        public virtual Addition Addition { get; set; }
    }
}