using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CayCanh.Models
{
    [Table("Configs")]
    public class Config
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Site_Name { get; set; }
        public string Hot_line { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public string Img { get; set; }
        [Required]
        public string Detail { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceSale { get; set; }
        public int Number { get; set; }
        [Required]
        public string MetaKey { get; set; }
        [Required]
        public string MetaDesc { get; set; }
    }
}