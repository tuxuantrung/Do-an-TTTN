using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CayCanh.Models;

namespace CayCanh.Controllers
{
    public class ModuleController : Controller
    {
        CayCanhDbContext db = new CayCanhDbContext();
        // GET: Module
        public ActionResult MainMenu()
        {
            var list = db.Menus.Where(m => m.Position == "mainmenu"
            && m.Status == 1 && m.ParentId == 0)
                .OrderBy(m => m.Orders).ToList();
            return View("MainMenu", list);
        }
        public ActionResult MainMenuSub(int id)
        {
            var menu = db.Menus.Find(id);
            if(db.Menus.Where(m=>m.Status == 1 && m.ParentId == id).Count() > 0)
            {
                var list = db.Menus.Where(m => m.Status == 1 && m.ParentId == id).OrderBy(m => m.Orders).ToList();
                ViewBag.Row_Menu = menu;
                return View("MainMenuSubdropdown", list);
            }
            else
            {
                return View("MainMenuSub", menu);
            }
        }
        public ActionResult Slideshow()
        {
            var list = db.Sliders.Where(m => m.Position == "Slideshow"
            && m.Status == 1)
                .OrderByDescending(m => m.CreateAt).ToList();
            return View("Slideshow", list);
        }
        public ActionResult ListCategory(int parentid = 0)
        {
            var list = db.Categories.Where(m => m.ParentId == parentid && m.Status == 1)
                .OrderBy(m => m.Orders).ToList();
            return View("ListCategory", list);
        }
        public ActionResult ProductBuyHot()
        {
            
            return View("ProductBuyHot");
        }
        public ActionResult ChooseByPrice()
        {

            return View("ChooseByPrice");
        }
        public ActionResult ProductInterested()
        {

            return View("ProductInterested");
        }
        //public ActionResult ProductBuyHot1()
        //{

        //    return View("ProductBuyHot1");
        //}
        public ActionResult ContactSidebar()
        {

            return View("ContactSidebar");
        }
    }
}