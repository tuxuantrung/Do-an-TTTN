using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CayCanh.Models
{
    [Table("Pages")]
    public class Page
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên loại trang đơn không để rỗng!")]
        public int CatId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        
        [Required]
        public int Number { get; set; }
        [Required(ErrorMessage = "Số lượng không để rỗng!")]
        public DateTime CreateAt { get; set; }
        public int CreateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateBy { get; set; }
        public int Status { get; set; }
    }
}