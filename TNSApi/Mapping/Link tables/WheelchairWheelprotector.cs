using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TNSApi.Mapping.Link_tables
{
    [Table("tbl_wheelchairs_wheelprotectors")]
    public class WheelchairWheelprotector
    {
        [Key]
        [Column("WheelchairWheelprotectorId")]
        public int Id { get; set; }
        [Required]
        public int WheelchairId { get; set; }
        [Required]
        public int WheelprotectorId { get; set; }
        [Required]
        public int AdditionId { get; set; }

        [ForeignKey("WheelprotectorId")]
        public virtual Wheelprotector WheelProtector { get; set; }
        [ForeignKey("AdditionId")]
        public virtual Addition Addition { get; set; }
    }
}