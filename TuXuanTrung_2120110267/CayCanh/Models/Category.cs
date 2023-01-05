using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CayCanh.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên loại sản phẩm không để rỗng!")]
        public string Name { get; set; }
        public string Slug { get; set; }
        public int ParentId { get; set; }
        public int Orders { get; set; }
        [Required(ErrorMessage = "Từ khóa SEO không để rỗng!")]
        public string MetaKey { get; set; }
        [Required(ErrorMessage = "Mô tả SEO không để rỗng!")]
        public string MetaDesc { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateBy { get; set; }
        public int Status { get; set; }
    }
}