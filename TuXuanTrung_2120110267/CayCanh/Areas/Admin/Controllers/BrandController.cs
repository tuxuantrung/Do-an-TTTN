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
    public class BrandController : BaseController
    {
        private CayCanhDbContext db = new CayCanhDbContext();

        // GET: Admin/Brand
        public ActionResult Index()
        {
            var list = db.Brands.Where(m => m.Status != 0).ToList();
            return View("Index", list);
            //return View(db.Brands.ToList());
        }

        // GET: Admin/Brand/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }
        // GET: Admin/Brand/Create
        public ActionResult Create()
        {
            ViewBag.ListCatId = new SelectList(db.Brands.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Brands.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View();
        }
        // POST: Admin/Brand/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                brand.Slug = MyString.str_slug(brand.Name);
                brand.CreateAt = DateTime.Now;
                brand.CreateBy = (Session["User_id"].ToString() != "") ? int.Parse(Session["User_id"].ToString()) : 1;
                if (brand.Orders == 1)
                {
                    brand.Orders = 1;
                }
                else
                {
                    brand.Orders += 1;
                }
                if (brand.ParentId == 1)
                {
                    brand.ParentId = 0;
                }

                db.Brands.Add(brand);
                if (db.SaveChanges() != 0)
                {
                    Link link = new Link();
                    link.Slug = brand.Slug;
                    link.TypeLink = "brand";
                    link.TableId = brand.Id;
                    db.Links.Add(link);
                    db.SaveChanges();
                }
                TempData["message"] = new MessageAlert("success", "Thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCatId = new SelectList(db.Brands.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Brands.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View(brand);
        }
        // GET: Admin/Brand/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCatId = new SelectList(db.Brands.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Brands.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View(brand);
        }
        // POST: Admin/Brand/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Slug,Orders,MetaKey,MetaDesc,CreateBy,CreateAt,UpdateAt,UpdateBy,Status")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                brand.Slug = MyString.str_slug(brand.Name);
                brand.UpdateAt = DateTime.Now;
                brand.UpdateBy = (Session["User_id"].ToString() != "") ? int.Parse(Session["User_id"].ToString()) : 1;
                if (brand.Orders == 1)
                {
                    brand.Orders = 1;
                }
                else
                {
                    brand.Orders += 1;
                }
                if (brand.ParentId == 1)
                {
                    brand.ParentId = 0;
                }

                db.Entry(brand).State = EntityState.Modified;
                if (db.SaveChanges() != 0)
                {
                    Link link = db.Links.Where(m => m.TypeLink == "Brand" && m.TableId == brand.Id).FirstOrDefault();
                    link.Slug = brand.Slug;
                    link.TypeLink = "Brand";
                    link.TableId = brand.Id;
                    db.Entry(brand).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.ListCatId = new SelectList(db.Brands.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Brands.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View(brand);
        }
        // GET: Admin/Brand/Destroy/5
        public ActionResult Destroy(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View("Destroy", brand);
        }
        // POST: Admin/Brand/Delete/5
        [HttpPost, ActionName("Destroy")]
        [ValidateAntiForgeryToken]
        public ActionResult DestroyConfirmed(int id)
        {
            Brand brand = db.Brands.Find(id);
            db.Brands.Remove(brand);
            if (db.SaveChanges() != 0)
            {
                Link link = db.Links.Where(m => m.TypeLink == "Brand" && m.TableId == id).FirstOrDefault();
                db.Links.Remove(link);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        // GET: Admin/Brand/Delete/5
        //Xóa vào thùng Status=0
        public ActionResult Delete(int? id)
        {
            Brand brand = db.Brands.Find(id);
            brand.Status = 0;
            brand.UpdateAt = DateTime.Now;
            brand.UpdateBy = (Session["User_id"].ToString() != "") ? int.Parse(Session["User_id"].ToString()) : 1;
            db.Entry(brand).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Admin/Brand/Status/5
        // Thay đổi trạng thái 1-->2 2-->1
        public ActionResult Status(int? id)
        {
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                TempData["message"] = new MessageAlert("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index");
            }
            brand.Status = (brand.Status == 2) ? 1 : 2;
            brand.UpdateAt = DateTime.Now;
            brand.UpdateBy = (Session["User_id"].ToString() != "") ? int.Parse(Session["User_id"].ToString()) : 1;
            db.Entry(brand).State = EntityState.Modified;
            db.SaveChanges();
            TempData["message"] = new MessageAlert("success", "Thành công");
            return RedirectToAction("Index");
        }
        // GET: Admin/Brand/Restore/5
        // Khôi phục Status=2
        public ActionResult Restore(int? id)
        {
            Brand brand = db.Brands.Find(id);
            brand.Status = 2;
            brand.UpdateAt = DateTime.Now;
            brand.UpdateBy = 1;
            db.Entry(brand).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Trash", "brand");
        }

        // GET: Admin/Brand/Trash/5
        //Hiện danh sách của danh mục 
        public ActionResult Trash()
        {
            var list = db.Brands.Where(m => m.Status == 0).ToList();
            return View("Trash", list);
        }
    }
}
