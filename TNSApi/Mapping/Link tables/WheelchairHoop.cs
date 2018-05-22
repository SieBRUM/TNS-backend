using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TNSApi.Mapping.Link_tables
{
    [Table("tbl_wheelchairs_hoops")]
    public class WheelchairHoop
    {
        [Key]
        [Column("WheelchairHoopId")]
        public int Id { get; set; }

        [Required]
        public int WheelchairId { get; set; }
        [Required]
        public int HoopId { get; set; }
        public int? AdditionId { get; set; }
        [Required]
        public bool IsWide { get; set; }

        [ForeignKey("HoopId")]
        public virtual Hoop Hoop { get; set; }
        [ForeignKey("AdditionId")]
        public virtual Addition Addition { get; set; }
    }
}