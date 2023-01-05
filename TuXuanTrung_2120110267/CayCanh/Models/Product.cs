using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CayCanh.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên loại sản phẩm không để rỗng!")]
        public int CatId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Img { get; set; }
        [Required]
        public string Detail { get; set; }
        public int Number { get; set; }
        [Required(ErrorMessage = "Số lượng không để rỗng!")]
        public decimal Price { get; set; }
        public decimal? PriceSale { get; set; }
        public int BrandId { get; set; }
        public string MetaKey { get; set; }
        [Required(ErrorMessage = "Mô tả SEO không để rỗng!")]
        public string MetaDesc { get; set; }
        public DateTime CreateAt { get; set; }
        public int CreateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateBy { get; set; }
        public int Status { get; set; }
    }
}