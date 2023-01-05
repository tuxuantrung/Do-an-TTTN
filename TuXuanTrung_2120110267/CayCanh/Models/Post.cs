using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CayCanh.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Img { get; set; }
        public string Name { get; set; }
        public int TopicId { get; set; }
        public string Slug { get; set; }
        public string PostType { get; set; }
        public int ParentId { get; set; }
        public int Orders { get; set; }
        public string Detail { get; set; }
        [Required]
        public string MetaKey { get; set; }
        [Required]
        public string MetaDesc { get; set; }

        public int CreateBy { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateBy { get; set; }
        public int Status { get; set; }
    }
}