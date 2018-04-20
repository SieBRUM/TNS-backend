using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TNSApi.Mapping
{
    [Table("tbl_frontwheels")]
    public class Frontwheel
    {
        [Key]
        [Column("FrontWheelId")]
        public int Id { get; set; }

        [Required]
        public double Price { get; set; }
        [Required]
        public string Description { get; set; }
    }
}