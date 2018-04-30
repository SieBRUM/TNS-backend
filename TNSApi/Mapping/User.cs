using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TNSApi.Mapping
{
    [Table("tbl_users")]
    public class User
    {
        [Key]
        [Column("UserId")]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public string AccessLevel { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Token { get; set; }
    }
}