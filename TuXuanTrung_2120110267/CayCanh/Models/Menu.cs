using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CayCanh.Models
{
    [Table("Menus")]
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public int ParentId { get; set; }
        public int Orders { get; set; }
        public string TypeMenu { get; set; }
        public string Position { get; set; }
        public int? TableId { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateBy { get; set; }
        public int Status { get; set; }
    }
}