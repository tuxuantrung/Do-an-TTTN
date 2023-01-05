using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CayCanh.Models
{
    [Table("Contacts")]
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string UseId { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Title { get; set; }
        [Required]
        public string Detail { get; set; }
        [Required]
        public string MetaKey { get; set; }
        [Required]
        public string MetaDesc { get; set; }
        public DateTime CreateAt { get; set; }
        public int CreateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateBy { get; set; }
        public int Status { get; set; }
    }
}