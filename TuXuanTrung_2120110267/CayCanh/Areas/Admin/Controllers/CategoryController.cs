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
    public class CategoryController : BaseController
    {
        private CayCanhDbContext db = new CayCanhDbContext();
        //private object[] id;

        // GET: Admin/Category
        public ActionResult Index()
        {
            var list = db.Categories.Where(m => m.Status != 0).ToList();
            return View("Index", list);
            //return View(db.Categories.ToList());
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            ViewBag.ListCatId = new SelectList(db.Categories.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Categories.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View();
        }
        // POST: Admin/Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = MyString.str_slug(category.Name);
                category.CreateAt = DateTime.Now;
                category.CreateBy = (Session["User_id"].ToString() != "") ? int.Parse(Session["User_id"].ToString()) : 1;
                if (category.Orders == 1)
                {
                    category.Orders = 1;
                }
                else
                {
                    category.Orders += 1;
                }
                if (category.ParentId == 1)
                {
                    category.ParentId = 0;
                }

                db.Categories.Add(category);
                if (db.SaveChanges() != 0)
                {
                    Link link = new Link();
                    link.Slug = category.Slug;
                    link.TypeLink = "category";
                    link.TableId = category.Id;
                    db.Links.Add(link);
                    db.SaveChanges();
                }
                TempData["message"] = new MessageAlert("success", "Thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCatId = new SelectList(db.Categories.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Categories.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View(category);
        }
        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCatId = new SelectList(db.Categories.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Categories.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View(category);
        }
        // POST: Admin/Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Slug,Orders,MetaKey,MetaDesc,CreateBy,CreateAt,UpdateAt,UpdateBy,Status")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = MyString.str_slug(category.Name);
                category.UpdateAt = DateTime.Now;
                category.UpdateBy = 1;
                if (category.Orders == 1)
                {
                    category.Orders = 1;
                }
                else
                {
                    category.Orders += 1;
                }
                if (category.ParentId == 1)
                {
                    category.ParentId = 0;
                }

                db.Entry(category).State = EntityState.Modified;
                if (db.SaveChanges() != 0)
                {
                    Link link = db.Links.Where(m => m.TypeLink == "category" && m.TableId == category.Id).FirstOrDefault();
                    link.Slug = category.Slug;
                    link.TypeLink = "category";
                    link.TableId = category.Id;
                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.ListCatId = new SelectList(db.Categories.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Categories.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View(category);
        }
        // GET: Admin/Category/Destroy/5
        public ActionResult Destroy(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View("Destroy", category);
        }
        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Destroy")]
        [ValidateAntiForgeryToken]
        public ActionResult DestroyConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            if (db.SaveChanges() != 0)
            {
                Link link = db.Links.Where(m => m.TypeLink == "category" && m.TableId == id).FirstOrDefault();
                db.Links.Remove(link);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        // GET: Admin/Category/Delete/5
        //Xóa vào thùng Status=0
        public ActionResult Delete(int? id)
        {
            Category category = db.Categories.Find(id);
            category.Status = 0;
            category.UpdateAt = DateTime.Now;
            category.UpdateBy = 1;
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Admin/Category/Status/5
        // Thay đổi trạng thái 1-->2 2-->1
        public ActionResult Status(int? id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                TempData["message"] = new MessageAlert("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index");
            }
            category.Status = (category.Status == 2) ? 1 : 2;
            category.UpdateAt = DateTime.Now;
            category.UpdateBy = 1;
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            TempData["message"] = new MessageAlert("success", "Thành công");
            return RedirectToAction("Index");
        }
        // GET: Admin/Category/Restore/5
        // Khôi phục Status=2
        public ActionResult Restore(int? id)
        {
            Category category = db.Categories.Find(id);
            category.Status = 2;
            category.UpdateAt = DateTime.Now;
            category.UpdateBy = 1;
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Trash", "Category");
        }

        // GET: Admin/Category/Trash/5
        //Hiện danh sách của danh mục 
        public ActionResult Trash()
        {
            var list = db.Categories.Where(m => m.Status == 0).ToList();
            return View("Trash", list);
        }
    }
}

