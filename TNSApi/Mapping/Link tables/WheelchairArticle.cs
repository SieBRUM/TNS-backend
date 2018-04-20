using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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
        public int ArticlesId { get; set; }
        [Required]
        public int AdditionId { get; set; }

        [ForeignKey("ArticlesId")]
        public virtual Article Article { get; set; }
        [ForeignKey("AdditionId")]
        public virtual Addition Addition { get; set; }
    }
}