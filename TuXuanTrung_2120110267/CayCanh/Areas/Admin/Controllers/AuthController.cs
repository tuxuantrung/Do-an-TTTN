using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CayCanh.Models;
namespace TruyenTranh.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        protected CayCanhDbContext db = new CayCanhDbContext();
        // GET: Admin/Auth
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            string error = null;
            User user = db.Users.Where(m => m.Status == 1 && m.Roles == 1 && (m.UserName == username || m.Email == username) && m.Password == password).FirstOrDefault();
            if (user == null)
            {
                error = "Thông tin đăng nhập không chính xác";
            }
            else
            {
                Session["UserAdmin"] = username;
                Session["User_Id"] = user.Id;
                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.Error = error;
            return View();
        }
        public ActionResult Logout()
        {
            Session["UserAdmin"] = "";
            Session["User_Id"] = "";
            return Redirect("~/Admin/Login");
        }
    }
}