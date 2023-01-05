using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CayCanh.Models;
using CayCanh.Libraries;
using System.Net;


namespace CayCanh.Controllers
{
    public class CartController : Controller
    {
        private CayCanhDbContext db = new CayCanhDbContext();
        // GET: Cart
        public ActionResult Index()
        {
            List<CartItem> listcart = null;
            if (Session["MyCart"] != "")
            {
                listcart = (List<CartItem>)Session["MyCart"];
            }               
            return View(listcart);
        }
        public ActionResult AddCart( int productid)
        {
            var product = db.Products.Find(productid);
            CartItem item = new CartItem(productid, product.Img, product.Name, 1, product.Price, product.Price);
            List<CartItem> listcart = null;
            if(Session["MyCart"] != "")
            {
                listcart = (List<CartItem>)Session["MyCart"];
                int count = listcart.Where(m => m.Id == productid).Count();
                if(count == 0)
                {
                    listcart.Add(item);
                }
            }
            else
            {
                listcart = new List<CartItem>();
                listcart.Add(item);
            }         
            Session["MyCart"] = listcart;
            return Redirect("~/gio-hang");            
        }
        public ActionResult CartDel(int productid)
        {
            CartItem.DelCart(productid);
            return RedirectToAction("Index", "Giohang");
        }
        //CartUpdate
        public ActionResult CartUpdate(FormCollection form)
        {
           if(!string.IsNullOrEmpty(form["CAPNHAT"]))
            {
                var listqty = form["qty"];
                var listarr = listqty.Split(',');
                XCart.UdateCart(listarr);
            }
            return RedirectToAction("Index", "Giohang");
        }
        public ActionResult CartDelAll()
        {
            XCart.DelCart();
            return RedirectToAction("Index", "Giohang");
        }

        public ActionResult Thanhtoan()
        {
            Order order = new Order();
            order.Code = 1234;
            order.DateOrder = DateTime.Now;
            order.DeliveryAddress = "HCM";
            order.DeliveryEmail = "xtrung@gmail.com";
            order.DeliveryName = "Xuan Trung";
            order.DeliveryPhone = "0925794461";
            order.Note = "Test";
            order.Status = 1;
            db.Orders.Add(order);
            if (db.SaveChanges() != 0)
            {
                List<CartItem> listcart = (List<CartItem>)Session["MyCart"];
                foreach(CartItem item in listcart)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderId = order.Id;
                    orderDetail.Price = item.Price;
                    orderDetail.Quantity = item.Qty;
                    orderDetail.ProductId = item.Id;
                    db.orderDetails.Add(orderDetail);
                    db.SaveChanges();
                }
                Session["MyCart"] = "";
            }
            return Redirect("~/gio-hang");
        }
    }   
}