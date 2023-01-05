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
    public class PostController : BaseController
    {
        private CayCanhDbContext db = new CayCanhDbContext();

        public dynamic SeachString { get; private set; }

        // GET: Admin/post
        public ActionResult Index()
        {
            var list = db.Posts.Where(m => m.Status != 0).ToList();
            return View("Index", list);
            //return View(db.Posts.ToList());
        }

        // GET: Admin/post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
        // GET: Admin/post/Create
        public ActionResult Create()
        {
            ViewBag.ListCatId = new SelectList(db.Posts.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Posts.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View();
        }
        // POST: Admin/post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                //uplaod file
                var file = Request.Files["Img"];
                string[] Extention = { ".jpg", ".png", ".gif" };
                if (file.ContentLength != 0)
                {
                    // có chọn file
                    var extention = file.FileName.Substring(file.FileName.LastIndexOf("."));
                    if (Extention.Contains(extention))
                    {
                        //hợp lệ
                        // đưa tập tin lên sever
                        var fileName = Path.Combine(Server.MapPath("~/Public/image/post/"), file.FileName);
                        file.SaveAs(fileName);
                        //lưu kết quả vào csdl
                        post.Img = file.FileName;
                        post.CreateAt = DateTime.Now;
                        post.CreateBy = /*(Session["User_id"].ToString() != "") ? int.Parse(Session["User_id"].ToString()) : */1;
                        db.Posts.Add(post);
                        db.SaveChanges();
                        TempData["message"] = new MessageAlert("success", "Thành công");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //không hợp lệ
                        TempData["message"] = new MessageAlert("danger", "Định dạng tập tin không hợp lệ");
                        return RedirectToAction("Create", "post");
                    }
                }
                else
                {
                    //chưa chọn file
                    TempData["message"] = new MessageAlert("danger", "Chưa chọn mẫu tin");
                    return RedirectToAction("Create", "post");
                }
                //end upload file
            }
            return View(post);
        }
        // GET: Admin/post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCatId = new SelectList(db.Posts.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Posts.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View(post);
        }
        // POST: Admin/post/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Slug,Orders,MetaKey,MetaDesc,CreateBy,CreateAt,UpdateAt,UpdateBy,Status")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.Slug = MyString.str_slug(post.Name);
                post.UpdateAt = DateTime.Now;
                post.UpdateBy = 1;
                if (post.Orders == 1)
                {
                    post.Orders = 1;
                }
                else
                {
                    post.Orders += 1;
                }
                if (post.ParentId== 1)
                {
                    post.ParentId = 0;
                }

                db.Entry(post).State = EntityState.Modified;
                if (db.SaveChanges() != 0)
                {
                    Link link = db.Links.Where(m => m.TypeLink == "post" && m.TableId == post.Id).FirstOrDefault();
                    link.Slug = post.Slug;
                    link.TypeLink = "post";
                    link.TableId = post.Id;
                    db.Entry(post).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.ListCatId = new SelectList(db.Posts.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Posts.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View(post);
        }
        // GET: Admin/post/Destroy/5
        public ActionResult Destroy(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View("Destroy", post);
        }
        // POST: Admin/post/Delete/5
        [HttpPost, ActionName("Destroy")]
        [ValidateAntiForgeryToken]
        public ActionResult DestroyConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            if (db.SaveChanges() != 0)
            {
                Link link = db.Links.Where(m => m.TypeLink == "post" && m.TableId == id).FirstOrDefault();
                db.Links.Remove(link);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        // GET: Admin/post/Delete/5
        //Xóa vào thùng Status=0
        public ActionResult Delete(int? id)
        {
            Post post = db.Posts.Find(id);
            post.Status = 0;
            post.UpdateAt = DateTime.Now;
            post.UpdateBy = 1;
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Admin/post/Status/5
        // Thay đổi trạng thái 1-->2 2-->1
        public ActionResult Status(int? id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                TempData["message"] = new MessageAlert("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index");
            }
            post.Status = (post.Status == 2) ? 1 : 2;
            post.UpdateAt = DateTime.Now;
            post.UpdateBy = 1;
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
            TempData["message"] = new MessageAlert("success", "Thành công");
            return RedirectToAction("Index");
        }
        // GET: Admin/post/Restore/5
        // Khôi phục Status=2
        public ActionResult Restore(int? id)
        {
            Post post = db.Posts.Find(id);
            post.Status = 2;
            post.UpdateAt = DateTime.Now;
            post.UpdateBy = 1;
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Trash", "post");
        }

        // GET: Admin/post/Trash/5
        //Hiện danh sách của danh mục 
        public ActionResult Trash()
        {
            var list = db.Posts.Where(m => m.Status == 0).ToList();
            return View("Trash", list);
        }
        //public ActionResult index(string currentFilter, string SearchString, int? page)
        //{
        //    var listpost = new List<post>();
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
        //        listpost = db.Posts.Where(m => m.Name.Contains(SearchString)).ToList();
        //    }
        //    else
        //    {
        //        //lấy all sản phẩm trong bảng post
        //        listpost = db.Posts.ToList();
        //    }
        //    ViewBag.CurrentFilter = SeachString;
        //    //số lượng post của 1 trang là 10
        //    int pageSize = 10;
        //    int pageNumber = (page ?? 1);
        //    //Sắp xếp theo Id sản phẩm, sản phẩm mới đưa lên đầu
        //    listpost = listpost.OrderByDescending(m => m.Id).ToList();
        //    return View(listpost.ToPagedList(pageNumber, pageSize));
        //}
    }
}
