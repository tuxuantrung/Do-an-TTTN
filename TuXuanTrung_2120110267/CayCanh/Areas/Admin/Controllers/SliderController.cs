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

namespace CayCanh.Areas.Admin.Controllers
{
    public class SliderController : Controller
    {
        private CayCanhDbContext db = new CayCanhDbContext();

        // GET: Admin/Slider
        public ActionResult Index()
        {
            var list = db.Sliders.Where(m => m.Status != 0).OrderByDescending(m => m.CreateBy).ToList();
            return View(list);
            //return View(db.Slider.ToList());
        }

        // GET: Admin/slider/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }
        // GET: Admin/slider/Create
        public ActionResult Create()
        {
            ViewBag.ListCatId = new SelectList(db.Sliders.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Sliders.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View();
        }
        // POST: Admin/slider/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Slider slider)
        {
            if (ModelState.IsValid)
            {
                //uplaod file
                var file = Request.Files["Img"];
                string[] Extention = { ".jpg", ".png", ".gif" };
                if(file.ContentLength != 0)
                {
                    // có chọn file
                    var extention = file.FileName.Substring(file.FileName.LastIndexOf("."));
                    if(Extention.Contains(extention))
                    {
                        //hợp lệ
                        // đưa tập tin lên sever
                        var fileName = Path.Combine(Server.MapPath("~/Public/image/slider/"), file.FileName);
                        file.SaveAs(fileName);
                        //lưu kết quả vào csdl
                        slider.Img = file.FileName;
                        slider.CreateAt = DateTime.Now;
                        slider.CreateBy = (Session["User_id"].ToString() != "") ? int.Parse(Session["User_id"].ToString()) : 1;
                        db.Sliders.Add(slider);
                        db.SaveChanges();
                        TempData["message"] = new MessageAlert("success", "Thành công");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //không hợp lệ
                        TempData["message"] = new MessageAlert("danger", "Định dạng tập tin không hợp lệ");
                        return RedirectToAction("Create", "Slider");
                    }    
                }
                else
                {
                    //chưa chọn file
                    TempData["message"] = new MessageAlert("danger", "Chưa chọn mẫu tin");
                    return RedirectToAction("Create", "Slider");
                }
                //end upload file
            }
            return View(slider);
        }
        // GET: Admin/slider/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCatId = new SelectList(db.Sliders.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Sliders.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View(slider);
        }
        // POST: Admin/slider/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Slug,Orders,MetaKey,MetaDesc,CreateBy,CreateAt,UpdateAt,UpdateBy,Status")] Slider slider)
        {
            if (ModelState.IsValid)
            {
                slider.Link = MyString.str_slug(slider.Name);
                slider.UpdateAt = DateTime.Now;
                slider.UpdateBy = 1;
                //if (slider.Orders == 1)
                //{
                //    slider.Orders = 1;
                //}
                //else
                //{
                //    slider.Orders += 1;
                //}
                //if (slider.ParentId == 1)
                //{
                //    slider.ParentId = 0;
                //}

                db.Entry(slider).State = EntityState.Modified;
                if (db.SaveChanges() != 0)
                {
                    Link link = db.Links.Where(m => m.TypeLink == "slider" && m.TableId == slider.Id).FirstOrDefault();
                    link.Slug = slider.Link;
                    link.TypeLink = "slider";
                    link.TableId = slider.Id;
                    db.Entry(slider).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.ListCatId = new SelectList(db.Sliders.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Sliders.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View(slider);
        }
        // GET: Admin/slider/Destroy/5
        public ActionResult Destroy(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View("Destroy", slider);
        }
        // POST: Admin/slider/Delete/5
        [HttpPost, ActionName("Destroy")]
        [ValidateAntiForgeryToken]
        public ActionResult DestroyConfirmed(int id)
        {
            Slider slider = db.Sliders.Find(id);
            db.Sliders.Remove(slider);
            if (db.SaveChanges() != 0)
            {
                Link link = db.Links.Where(m => m.TypeLink == "slider" && m.TableId == id).FirstOrDefault();
                db.Links.Remove(link);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        // GET: Admin/slider/Delete/5
        //Xóa vào thùng Status=0
        public ActionResult Delete(int? id)
        {
            Slider slider = db.Sliders.Find(id);
            slider.Status = 0;
            slider.UpdateAt = DateTime.Now;
            slider.UpdateBy = 1;
            db.Entry(slider).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Admin/slider/Status/5
        // Thay đổi trạng thái 1-->2 2-->1
        public ActionResult Status(int? id)
        {
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                TempData["message"] = new MessageAlert("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index");
            }
            slider.Status = (slider.Status == 2) ? 1 : 2;
            slider.UpdateAt = DateTime.Now;
            slider.UpdateBy = 1;
            db.Entry(slider).State = EntityState.Modified;
            db.SaveChanges();
            TempData["message"] = new MessageAlert("success", "Thành công");
            return RedirectToAction("Index");
        }
        // GET: Admin/slider/Restore/5
        // Khôi phục Status=2
        public ActionResult Restore(int? id)
        {
            Slider slider = db.Sliders.Find(id);
            slider.Status = 2;
            slider.UpdateAt = DateTime.Now;
            slider.UpdateBy = 1;
            db.Entry(slider).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Trash", "slider");
        }

        // GET: Admin/slider/Trash/5
        //Hiện danh sách của danh mục 
        public ActionResult Trash()
        {
            var list = db.Sliders.Where(m => m.Status == 0).ToList();
            return View("Trash", list);
        }
    }
}
