using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CayCanh.Models;
using CayCanh.Libraries;


namespace CayCanh.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        private CayCanhDbContext db = new CayCanhDbContext();
        //private object[] id;

        // GET: Admin/Menu
        public ActionResult Index()
        {
            var list = db.Menus.Where(m => m.Status != 0).ToList();
            return View("Index", list);
            //return View(db.Menus.ToList());
        }

        // GET: Admin/menu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }
        // GET: Admin/menu/Create
        public ActionResult Create()
        {
            ViewBag.ListCatId = new SelectList(db.Menus.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Menus.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View();
        }
        // POST: Admin/menu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                menu.Link = MyString.str_slug(menu.Name);
                menu.CreateAt = DateTime.Now;
                menu.CreateBy = (Session["User_id"].ToString() != "") ? int.Parse(Session["User_id"].ToString()) : 1;
                if (menu.Orders == 1)
                {
                    menu.Orders = 1;
                }
                else
                {
                    menu.Orders += 1;
                }
                if (menu.ParentId == 1)
                {
                    menu.ParentId = 0;
                }

                db.Menus.Add(menu);
                if (db.SaveChanges() != 0)
                {
                    Link link = new Link();
                    link.Slug = menu.Link;
                    link.TypeLink = "menu";
                    link.TableId = menu.Id;
                    db.Links.Add(link);
                    db.SaveChanges();
                }
                TempData["message"] = new MessageAlert("success", "Thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCatId = new SelectList(db.Menus.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Menus.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View(menu);
        }
        // GET: Admin/menu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCatId = new SelectList(db.Menus.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Menus.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View(menu);
        }
        // POST: Admin/menu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Slug,Orders,MetaKey,MetaDesc,CreateBy,CreateAt,UpdateAt,UpdateBy,Status")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                menu.Link = MyString.str_slug(menu.Name);
                menu.UpdateAt = DateTime.Now;
                menu.UpdateBy = 1;
                if (menu.Orders == 1)
                {
                    menu.Orders = 1;
                }
                else
                {
                    menu.Orders += 1;
                }
                if (menu.ParentId == 1)
                {
                    menu.ParentId = 0;
                }

                db.Entry(menu).State = EntityState.Modified;
                if (db.SaveChanges() != 0)
                {
                    Link link = db.Links.Where(m => m.TypeLink == "menu" && m.TableId == menu.Id).FirstOrDefault();
                    link.Slug = menu.Link;
                    link.TypeLink = "menu";
                    link.TableId = menu.Id;
                    db.Entry(menu).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.ListCatId = new SelectList(db.Menus.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Menus.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View(menu);
        }
        // GET: Admin/menu/Destroy/5
        public ActionResult Destroy(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View("Destroy", menu);
        }
        // POST: Admin/menu/Delete/5
        [HttpPost, ActionName("Destroy")]
        [ValidateAntiForgeryToken]
        public ActionResult DestroyConfirmed(int id)
        {
            Menu menu = db.Menus.Find(id);
            db.Menus.Remove(menu);
            if (db.SaveChanges() != 0)
            {
                Link link = db.Links.Where(m => m.TypeLink == "menu" && m.TableId == id).FirstOrDefault();
                db.Links.Remove(link);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        // GET: Admin/menu/Delete/5
        //Xóa vào thùng Status=0
        public ActionResult Delete(int? id)
        {
            Menu menu = db.Menus.Find(id);
            menu.Status = 0;
            menu.UpdateAt = DateTime.Now;
            menu.UpdateBy = 1;
            db.Entry(menu).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Admin/menu/Status/5
        // Thay đổi trạng thái 1-->2 2-->1
        public ActionResult Status(int? id)
        {
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                TempData["message"] = new MessageAlert("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index");
            }
            menu.Status = (menu.Status == 2) ? 1 : 2;
            menu.UpdateAt = DateTime.Now;
            menu.UpdateBy = 1;
            db.Entry(menu).State = EntityState.Modified;
            db.SaveChanges();
            TempData["message"] = new MessageAlert("success", "Thành công");
            return RedirectToAction("Index");
        }
        // GET: Admin/menu/Restore/5
        // Khôi phục Status=2
        public ActionResult Restore(int? id)
        {
            Menu menu = db.Menus.Find(id);
            menu.Status = 2;
            menu.UpdateAt = DateTime.Now;
            menu.UpdateBy = 1;
            db.Entry(menu).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Trash", "menu");
        }

        // GET: Admin/menu/Trash/5
        //Hiện danh sách của danh mục 
        public ActionResult Trash()
        {
            var list = db.Menus.Where(m => m.Status == 0).ToList();
            return View("Trash", list);
        }
    }
}

