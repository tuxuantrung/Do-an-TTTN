using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CayCanh.Libraries;

namespace CayCanh.Libraries
{
    public class XCart
    {
        public void DelCart(int? productid = null)
        {
            if(productid != null)
            {
                if (!System.Web.HttpContext.Current.Session["MyCart"].Equals(""))
                {
                    List<CartItem> listcart = (List<CartItem>)System.Web.HttpContext.Current.Session["MyCart"];
                    int vt = 0;
                    foreach (var item in listcart)
                    {
                        if (item.Id == productid)
                        {
                            listcart.RemoveAt(vt);
                            break;
                        }
                        vt++;
                    }
                    System.Web.HttpContext.Current.Session["MyCart"] = listcart;
                }

            }
            else
            {
                System.Web.HttpContext.Current.Session["MyCart"] = "";
            }
               
        }
        public void UpdateCart(string[] arrqty)
        {
            List<CartItem> listcart = this.getCart();
            int vt = 0;
            foreach(CartItem cartitem in listcart)
            {
                listcart[vt].Qty = int.Parse(arrqty[vt]);
                listcart[vt].Amount = (listcart[vt].Qty * listcart[vt].Price);
                vt++;
            }
            System.Web.HttpContext.Current.Session["MyCart"] = listcart;
        }
        public List<CartItem> getCart()
        {
            if(System.Web.HttpContext.Current.Session["MyCart"].Equals(""))
            {
                return null;
            }
            return (List<CartItem>)System.Web.HttpContext.Current.Session["MyCart"];
        }
       

        internal static void UdateCart(string[] listarr)
        {
            throw new NotImplementedException();
        }

        internal static void DelCart()
        {
            throw new NotImplementedException();
        }
    }
}