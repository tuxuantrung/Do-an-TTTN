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
    public class TopicController : BaseController
    {
        private CayCanhDbContext db = new CayCanhDbContext();

        // GET: Admin/Topic
        public ActionResult Index()
        {
            var list = db.Topics.Where(m => m.Status != 0).ToList();
            return View("Index", list);
            //return View(db.Topics.ToList());
        }

        // GET: Admin/Topics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }
        // GET: Admin/Topics/Create
        public ActionResult Create()
        {
            ViewBag.ListCatId = new SelectList(db.Topics.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Topics.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View();
        }
        // POST: Admin/Topics/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Topic topic)
        {
            if (ModelState.IsValid)
            {
                topic.Slug = MyString.str_slug(topic.Name);
                topic.CreateAt = DateTime.Now;
                topic.CreateBy = 1;
                if (topic.Orders == 1)
                {
                    topic.Orders = 1;
                }
                else
                {
                    topic.Orders += 1;
                }
                if (topic.ParentId == 1)
                {
                    topic.ParentId = 0;
                }

                db.Topics.Add(topic);
                if (db.SaveChanges() != 0)
                {
                    Link link = new Link();
                    link.Slug = topic.Slug;
                    link.TypeLink = "Topics";
                    link.TableId = topic.Id;
                    db.Links.Add(link);
                    db.SaveChanges();
                }
                TempData["message"] = new MessageAlert("success", "Thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCatId = new SelectList(db.Topics.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Topics.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View(topic);
        }
        // GET: Admin/Topics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCatId = new SelectList(db.Topics.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Topics.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View(topic);
        }
        // POST: Admin/Topics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Slug,Orders,MetaKey,MetaDesc,CreateBy,CreateAt,UpdateAt,UpdateBy,Status")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                topic.Slug = MyString.str_slug(topic.Name);
                topic.UpdateAt = DateTime.Now;
                topic.UpdateBy = 1;
                if (topic.Orders == 1)
                {
                    topic.Orders = 1;
                }
                else
                {
                    topic.Orders += 1;
                }
                if (topic.ParentId == 1)
                {
                    topic.ParentId = 0;
                }

                db.Entry(topic).State = EntityState.Modified;
                if (db.SaveChanges() != 0)
                {
                    Link link = db.Links.Where(m => m.TypeLink == "Topics" && m.TableId == topic.Id).FirstOrDefault();
                    link.Slug = topic.Slug;
                    link.TypeLink = "Topics";
                    link.TableId = topic.Id;
                    db.Entry(topic).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.ListCatId = new SelectList(db.Topics.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Topics.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View(topic);
        }
        // GET: Admin/Topics/Destroy/5
        public ActionResult Destroy(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View("Destroy", topic);
        }
        // POST: Admin/Topics/Delete/5
        [HttpPost, ActionName("Destroy")]
        [ValidateAntiForgeryToken]
        public ActionResult DestroyConfirmed(int id)
        {
            Topic topic = db.Topics.Find(id);
            db.Topics.Remove(topic);
            if (db.SaveChanges() != 0)
            {
                Link link = db.Links.Where(m => m.TypeLink == "Topics" && m.TableId == id).FirstOrDefault();
                db.Links.Remove(link);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        // GET: Admin/Topics/Delete/5
        //Xóa vào thùng Status=0
        public ActionResult Delete(int? id)
        {
            Topic topic = db.Topics.Find(id);
            topic.Status = 0;
            topic.UpdateAt = DateTime.Now;
            topic.UpdateBy = 1;
            db.Entry(topic).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Admin/Topics/Status/5
        // Thay đổi trạng thái 1-->2 2-->1
        public ActionResult Status(int? id)
        {
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                TempData["message"] = new MessageAlert("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index");
            }
            topic.Status = (topic.Status == 2) ? 1 : 2;
            topic.UpdateAt = DateTime.Now;
            topic.UpdateBy = 1;
            db.Entry(topic).State = EntityState.Modified;
            db.SaveChanges();
            TempData["message"] = new MessageAlert("success", "Thành công");
            return RedirectToAction("Index");
        }
        // GET: Admin/Topics/Restore/5
        // Khôi phục Status=2
        public ActionResult Restore(int? id)
        {
            Topic topic = db.Topics.Find(id);
            topic.Status = 2;
            topic.UpdateAt = DateTime.Now;
            topic.UpdateBy = 1;
            db.Entry(topic).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Trash", "Topics");
        }

        // GET: Admin/Topics/Trash/5
        //Hiện danh sách của danh mục 
        public ActionResult Trash()
        {
            var list = db.Topics.Where(m => m.Status == 0).ToList();
            return View("Trash", list);
        }
    }
}
