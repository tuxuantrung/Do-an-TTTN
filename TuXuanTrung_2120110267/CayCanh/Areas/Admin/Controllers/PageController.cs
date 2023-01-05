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
using System.IO;
using PagedList;

namespace CayCanh.Areas.Admin.Controllers
{
    public class PageController : BaseController
    {
        private CayCanhDbContext db = new CayCanhDbContext();

        public dynamic SeachString { get; private set; }

        // GET: Admin/page
        public ActionResult Index()
        {
            var list = db.Pages.Where(m => m.Status != 0).ToList();
            return View("Index", list);
            //return View(db.Pages.ToList());
        }

        // GET: Admin/page/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = db.Pages.Find(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }
        // GET: Admin/page/Create
        public ActionResult Create()
        {
            ViewBag.ListCatId = new SelectList(db.Pages.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Pages.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View();
        }
        // page: Admin/page/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = MyString.str_slug(page.Name);
                page.CreateAt = DateTime.Now;
                page.CreateBy = (Session["User_id"].ToString() != "") ? int.Parse(Session["User_id"].ToString()) : 1;
                //if (page.Number == 1)
                //{
                //    page.Number = 1;
                //}
                //else
                //{
                //    page.Number += 1;
                //}
                //if (page.CatId == 1)
                //{
                //    page.CatId = 0;
                //}

                db.Pages.Add(page);
                if (db.SaveChanges() != 0)
                {
                    Link link = new Link();
                    link.Slug = page.Slug;
                    link.TypeLink = "category";
                    link.TableId = page.Id;
                    db.Links.Add(link);
                    db.SaveChanges();
                }
                TempData["message"] = new MessageAlert("success", "Thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCatId = new SelectList(db.Categories.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Categories.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View(page);
        }
        // GET: Admin/page/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = db.Pages.Find(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCatId = new SelectList(db.Pages.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Pages.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View(page);
        }
        // page: Admin/page/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Slug,Orders,MetaKey,MetaDesc,CreateBy,CreateAt,UpdateAt,UpdateBy,Status")] Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = MyString.str_slug(page.Name);
                page.UpdateAt = DateTime.Now;
                page.UpdateBy = 1;
                if (page.Number == 1)
                {
                    page.Number = 1;
                }
                else
                {
                    page.Number += 1;
                }
                if (page.CatId == 1)
                {
                    page.CatId = 0;
                }

                db.Entry(page).State = EntityState.Modified;
                if (db.SaveChanges() != 0)
                {
                    Link link = db.Links.Where(m => m.TypeLink == "page" && m.TableId == page.Id).FirstOrDefault();
                    link.Slug = page.Slug;
                    link.TypeLink = "page";
                    link.TableId = page.Id;
                    db.Entry(page).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.ListCatId = new SelectList(db.Pages.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Pages.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View(page);
        }
        // GET: Admin/page/Destroy/5
        public ActionResult Destroy(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = db.Pages.Find(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View("Destroy", page);
        }
        // page: Admin/page/Delete/5
        [HttpPost, ActionName("Destroy")]
        [ValidateAntiForgeryToken]
        public ActionResult DestroyConfirmed(int id)
        {
            Page page = db.Pages.Find(id);
            db.Pages.Remove(page);
            if (db.SaveChanges() != 0)
            {
                Link link = db.Links.Where(m => m.TypeLink == "page" && m.TableId == id).FirstOrDefault();
                db.Links.Remove(link);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        // GET: Admin/page/Delete/5
        //Xóa vào thùng Status=0
        public ActionResult Delete(int? id)
        {
            Page page = db.Pages.Find(id);
            page.Status = 0;
            page.UpdateAt = DateTime.Now;
            page.UpdateBy = 1;
            db.Entry(page).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Admin/page/Status/5
        // Thay đổi trạng thái 1-->2 2-->1
        public ActionResult Status(int? id)
        {
            Page page = db.Pages.Find(id);
            if (page == null)
            {
                TempData["message"] = new MessageAlert("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index");
            }
            page.Status = (page.Status == 2) ? 1 : 2;
            page.UpdateAt = DateTime.Now;
            page.UpdateBy = 1;
            db.Entry(page).State = EntityState.Modified;
            db.SaveChanges();
            TempData["message"] = new MessageAlert("success", "Thành công");
            return RedirectToAction("Index");
        }
        // GET: Admin/page/Restore/5
        // Khôi phục Status=2
        public ActionResult Restore(int? id)
        {
            Page page = db.Pages.Find(id);
            page.Status = 2;
            page.UpdateAt = DateTime.Now;
            page.UpdateBy = 1;
            db.Entry(page).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Trash", "page");
        }

        // GET: Admin/page/Trash/5
        //Hiện danh sách của danh mục 
        public ActionResult Trash()
        {
            var list = db.Pages.Where(m => m.Status == 0).ToList();
            return View("Trash", list);
        }
        //public ActionResult index(string currentFilter, string SearchString, int? page)
        //{
        //    var listpage = new List<page>();
        //    if(SearchString != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        SearchString = currentFilter;
        //    }
        //    if (!string.IsNullOrEmpty(SearchString))
        //    {
        //        //lấy ds sản phẩm theo từ khóa tìm kiếm
        //        listpage = db.Pages.Where(m => m.Name.Contains(SearchString)).ToList();
        //    }
        //    else
        //    {
        //        //lấy all sản phẩm trong bảng page
        //        listpage = db.Pages.ToList();
        //    }
        //    ViewBag.CurrentFilter = SeachString;
        //    //số lượng page của 1 trang là 10
        //    int pageSize = 10;
        //    int pageNumber = (page ?? 1);
        //    //Sắp xếp theo Id sản phẩm, sản phẩm mới đưa lên đầu
        //    listpage = listpage.OrderByDescending(m => m.Id).ToList();
        //    return View(listpage.ToPagedList(pageNumber, pageSize));
        //}
    }
}
