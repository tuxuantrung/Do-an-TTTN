using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace CayCanh.Libraries
{
    public class CartItem
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Qty { get; set; }
        public float Price { get; set; }
        public float Amount { get; set; }
        public CartItem(int productid, string img) { }
        public CartItem(int id, string img, string name, int qty, float price, float amount)
        {
            this.Id = id;
            this.Image = img;
            this.Name = name;
            this.Qty = qty;
            this.Price = price;
            this.Amount = amount;

        }

        internal static void DelCart(int productid)
        {
            throw new NotImplementedException();
        }
    }
}