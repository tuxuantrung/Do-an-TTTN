using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CayCanh.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int Code { get; set; }
        public string DeliveryName { get; set; }
        public string DeliveryEmail { get; set; }
        public string DeliveryPhone { get; set; }
        public string DeliveryAddress { get; set; }

        public string Note { get; set; }
        public int? UserId { get; set; }
        public DateTime? DateOrder { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateBy { get; set; }
        public int Status { get; set; }
    }
}