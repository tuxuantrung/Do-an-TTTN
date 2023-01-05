using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CayCanh.Models
{
    [Table("Links")]
    public class Link
    {
        [Key]
        public int Id { get; set; }
        public string Slug { get; set; }
        public string TypeLink { get; set; }
        public int TableId { get; set; }
    }
}