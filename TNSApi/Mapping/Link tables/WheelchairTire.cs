using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TNSApi.Mapping.Link_tables
{
    [Table("tbl_wheelchairs_tires")]
    public class WheelchairTire
    {
        [Key]
        [Column("WheelchairTireId")]
        public int Id { get; set; }

        [Required]
        public int WheelchairId { get; set; }
        [Required]
        public int TireId { get; set; }
        public int? AdditionId { get; set; }

        [ForeignKey("TireId")]
        public virtual Tire Tire { get; set; }
        [ForeignKey("AdditionId")]
        public virtual Addition Addition { get; set; }
    }
}