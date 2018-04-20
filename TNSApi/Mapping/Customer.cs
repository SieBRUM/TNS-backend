using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TNSApi.Mapping
{
    [Table("tbl_customers")]
    public class Customer
    {
        [Key]
        [Column("CustomerId")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Column("EmailAddress")]
        public string Email { get; set; }
        [Required]
        [Column("TelephoneNumber")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City{ get; set; }
        [Required]
        public string Zipcode { get; set; }
    }
}