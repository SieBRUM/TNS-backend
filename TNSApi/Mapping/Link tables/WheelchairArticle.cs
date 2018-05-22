using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TNSApi.Mapping.Link_tables
{
    [Table("tbl_wheelchair_articles")]
    public class WheelchairArticle
    {
        [Key]
        [Column("WheelchairArticleId")]
        public int Id { get; set; }
        [Required]
        public int WheelchairId { get; set; }
        [Required]
        public int ArticleId { get; set; }
        public int? AdditionId { get; set; }

        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }
        [ForeignKey("AdditionId")]
        public virtual Addition Addition { get; set; }
    }
}