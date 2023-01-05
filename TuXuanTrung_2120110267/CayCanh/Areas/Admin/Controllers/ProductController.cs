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
    public class ProductController : BaseController
    {
        private CayCanhDbContext db = new CayCanhDbContext();

        public dynamic SeachString { get; private set; }

        // GET: Admin/Product
        public ActionResult Index()
        {
            var list = db.Products.Where(m => m.Status != 0).ToList();
            return View("Index", list);
            //return View(db.Products.ToList());
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.ListCatId = new SelectList(db.Products.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Products.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View();
        }
        // POST: Admin/Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
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
                        var fileName = Path.Combine(Server.MapPath("~/Public/image/product/"), file.FileName);
                        file.SaveAs(fileName);
                        //lưu kết quả vào csdl
                        product.Img = file.FileName;
                        product.CreateAt = DateTime.Now;
                        product.CreateBy = /*(Session["User_id"].ToString() != "") ? int.Parse(Session["User_id"].ToString()) : */1;
                        db.Products.Add(product);
                        db.SaveChanges();
                        TempData["message"] = new MessageAlert("success", "Thành công");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //không hợp lệ
                        TempData["message"] = new MessageAlert("danger", "Định dạng tập tin không hợp lệ");
                        return RedirectToAction("Create", "Product");
                    }
                }
                else
                {
                    //chưa chọn file
                    TempData["message"] = new MessageAlert("danger", "Chưa chọn mẫu tin");
                    return RedirectToAction("Create", "Product");
                }
                //end upload file
            }
            return View(product);
        }
        // GET: Admin/product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCatId = new SelectList(db.Products.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Products.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View(product);
        }
        // POST: Admin/product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Slug,Orders,MetaKey,MetaDesc,CreateBy,CreateAt,UpdateAt,UpdateBy,Status")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Slug = MyString.str_slug(product.Name);
                product.UpdateAt = DateTime.Now;
                product.UpdateBy = 1;
                if (product.Number == 1)
                {
                    product.Number = 1;
                }
                else
                {
                    product.Number += 1;
                }
                if (product.CatId == 1)
                {
                    product.CatId = 0;
                }

                db.Entry(product).State = EntityState.Modified;
                if (db.SaveChanges() != 0)
                {
                    Link link = db.Links.Where(m => m.TypeLink == "product" && m.TableId == product.Id).FirstOrDefault();
                    link.Slug = product.Slug;
                    link.TypeLink = "product";
                    link.TableId = product.Id;
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.ListCatId = new SelectList(db.Products.Where(m => m.Status != 0).ToList(), "Id", "Name");
            ViewBag.ListOrder = new SelectList(db.Products.Where(m => m.Status != 0).ToList(), "Orders", "Name");
            return View(product);
        }
        // GET: Admin/product/Destroy/5
        public ActionResult Destroy(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View("Destroy", product);
        }
        // POST: Admin/product/Delete/5
        [HttpPost, ActionName("Destroy")]
        [ValidateAntiForgeryToken]
        public ActionResult DestroyConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            if (db.SaveChanges() != 0)
            {
                Link link = db.Links.Where(m => m.TypeLink == "product" && m.TableId == id).FirstOrDefault();
                db.Links.Remove(link);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        // GET: Admin/product/Delete/5
        //Xóa vào thùng Status=0
        public ActionResult Delete(int? id)
        {
            Product product = db.Products.Find(id);
            product.Status = 0;
            product.UpdateAt = DateTime.Now;
            product.UpdateBy = 1;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Admin/product/Status/5
        // Thay đổi trạng thái 1-->2 2-->1
        public ActionResult Status(int? id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                TempData["message"] = new MessageAlert("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index");
            }
            product.Status = (product.Status == 2) ? 1 : 2;
            product.UpdateAt = DateTime.Now;
            product.UpdateBy = 1;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            TempData["message"] = new MessageAlert("success", "Thành công");
            return RedirectToAction("Index");
        }
        // GET: Admin/product/Restore/5
        // Khôi phục Status=2
        public ActionResult Restore(int? id)
        {
            Product product = db.Products.Find(id);
            product.Status = 2;
            product.UpdateAt = DateTime.Now;
            product.UpdateBy = 1;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Trash", "Product");
        }

        // GET: Admin/product/Trash/5
        //Hiện danh sách của danh mục 
        public ActionResult Trash()
        {
            var list = db.Products.Where(m => m.Status == 0).ToList();
            return View("Trash", list);
        }
        //public ActionResult index(string currentFilter, string SearchString, int? page)
        //{
        //    var listProduct = new List<Product>();
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
        //        listProduct = db.Products.Where(m => m.Name.Contains(SearchString)).ToList();
        //    }
        //    else
        //    {
        //        //lấy all sản phẩm trong bảng Product
        //        listProduct = db.Products.ToList();
        //    }
        //    ViewBag.CurrentFilter = SeachString;
        //    //số lượng product của 1 trang là 10
        //    int pageSize = 10;
        //    int pageNumber = (page ?? 1);
        //    //Sắp xếp theo Id sản phẩm, sản phẩm mới đưa lên đầu
        //    listProduct = listProduct.OrderByDescending(m => m.Id).ToList();
        //    return View(listProduct.ToPagedList(pageNumber, pageSize));
        //}
    }
}
