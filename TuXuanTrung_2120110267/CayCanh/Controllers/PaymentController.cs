using CayCanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CayCanh.Controllers
{
    public class PaymentController : Controller
    {
        private CayCanhDbContext db = new CayCanhDbContext();
        // GET: Payment
        public ActionResult Index()
        {
            if(Session["UserId"] ==null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                //Lấy thông tin từ giỏ hàng từ biến session
                var listCart = (List<CartModel>)Session["cart"];
                //gán dữ liệu cho Ordel
                Order objOrder = new Order();
                objOrder.DeliveryName = "DonHan-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.UserId = int.Parse(Session["UserId"].ToString());
                objOrder.Created0nUtc = DateTime.Now;
                objOrder.Status = 1;
                CayCanhDbContext.Orders.Add(objOrder);
                //Luư thông tin dữ liệu vào bảng order
                CayCanhDbContext.SaveChanges();
                //Lấy OrderId vừa mới tạo để lưu vào vào bảng OrderDetail
                int intOrderId = objOrder.Id;
                List<OrderDetail> listOrderDetail = new List<OrderDetail>();
                foreach(var item in listCart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.Quantity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.Product.Id;
                    listOrderDetail.Add(obj);
                }
                objCayCanhEntities.OrderDetails.AddRange(listOrderDetail);
                ojbCayCanhEntities.saveChanges();
            }
            return View();
        }
    }
}