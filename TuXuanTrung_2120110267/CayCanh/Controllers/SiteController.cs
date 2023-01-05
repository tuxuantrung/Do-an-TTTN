using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CayCanh.Models;
using CayCanh.Libraries;

namespace CayCanh.Controllers
{
    public class SiteController : Controller
    {
        private CayCanhDbContext db = new CayCanhDbContext();
        private int? page;
        private int totalPosts;

        // GET: Site
        public ActionResult Index(string slug = null, string SearchString = "")
        {
            if(SearchString != "")
            {
                var product = db.Products.Where(m => m.Name.ToUpper().Contains(SearchString.ToUpper()));
                return View(product.ToList());
            }    
            //var list = db.Products.ToList();
            //ViewBag.SoLuong = list.Count;
            //return View();
            else if (slug == null)
            {
                return this.Home();
            }
            else
            {
                Link link = db.Links.Where(m => m.Slug == slug).FirstOrDefault();
                if (link != null)
                {
                    var typelink = link.TypeLink;
                    switch (typelink)
                    {
                        case "category":
                            {
                                return this.ProductCategory(slug);
                            }
                        case "brand":
                            {
                                return this.ProductBrand(slug);
                            }
                        case "topic":
                            {
                                return this.PostTopic(slug);
                            }
                        case "page":
                            {
                                return this.PostPage(slug);
                            }
                        case "default":
                            {
                                return this.Error404(slug);
                            }
                    }
                }
                else
                {
                    //slug có trong bảng Product
                    Product product = db.Products.Where(m => m.Slug == slug && m.Status == 1).FirstOrDefault();
                    if (product != null)
                    {
                        return this.ProductDetail(product);
                    }
                    else
                    {
                        Post post = db.Posts.Where(m => m.Slug == slug && m.Status == 1 && m.PostType == "post").FirstOrDefault();
                        if (post != null)
                        {
                            return this.PostDetail(post);
                        }
                        else
                        {
                            return this.Error404(slug);
                        }
                    }
                    //Tìm slug không có trong bản Link
                }
            }
            return this.Error404(slug);
        }
        //Trang chủ
        public ActionResult Home()
        {
            var list = db.Categories.Where(m => m.ParentId == 0 && m.Status == 1).ToList();
            return View("Home", list);
        }
        public ActionResult ProductHome(int catid)
        {
            var listcatid = new List<int>(); //danh sach
            listcatid.Add(catid); //thêm phần tử vào danh sách
            var categorys = db.Categories.Where(m => m.ParentId == catid && m.Status == 1).ToList();
            if(categorys.Count>0)
            {
                foreach(var cat in categorys)
                {
                    listcatid.Add(cat.Id);
                    var categorys1 = db.Categories.Where(m => m.ParentId == cat.Id && m.Status == 1).ToList();
                    if (categorys1.Count > 0)
                    {
                        foreach (var cat1 in categorys1)
                        {
                            listcatid.Add(cat1.Id);
                        }
                    }
                }
            }
            var list = db.Products.Where(m => m.Status == 1 && listcatid.Contains(m.CatId)).OrderByDescending(m=>m.CreateAt).Take(4).ToList();
            return View("ProductHome", list);
        }

        //nhóm action Product
        public ActionResult Product(int? page)
        {
            int pageNumber = page ?? 1;
            var model = new List<Product>();
            model = db.Products.Where(m => m.Status == 1).OrderByDescending(m => m.CreateAt).Skip(12 * (pageNumber - 1)).Take(12).ToList();
            var totalProducts = db.Products.Where(m => m.Status == 1).Count();
            ViewBag.TotalPage = (totalProducts / 12) + (totalProducts % 12 == 0 ? 0 : 1);
            return View("Product" , model);
        }
        public ActionResult ProductCategory(string slug)
        {
            int pageNumber = page ?? 1;
            var model = new List<Product>();
            model = db.Products.Where(m => m.Status == 1).OrderByDescending(m => m.CreateAt).Skip(12 * (pageNumber - 1)).Take(12).ToList();
            var totalProducts = db.Products.Where(m => m.Status == 1).Count();
            ViewBag.TotalPage = (totalProducts / 12) + (totalProducts % 12 == 0 ? 0 : 1);

            var category = db.Categories.Where(m => m.Status == 1 && m.Slug == slug).FirstOrDefault();
            if (category != null)
            {
                int catid = category.Id;
                ViewBag.Category = category;
                var listcatid = new List<int>(); //danh sach
                listcatid.Add(catid); //thêm phần tử vào danh sách      
                var categorys = db.Categories.Where(m => m.ParentId == catid && m.Status == 1).ToList();
                if (categorys.Count > 0)
                {
                    foreach (var cat in categorys)
                    {
                        listcatid.Add(cat.Id);
                        var categorys1 = db.Categories.Where(m => m.ParentId == cat.Id && m.Status == 1).ToList();
                        if (categorys1.Count > 0)
                        {
                            foreach (var cat1 in categorys1)
                            {
                                listcatid.Add(cat1.Id);
                            }
                        }
                    }
                }

                var product = db.Products.Where(m => m.Status == 1 && listcatid.Contains(m.CatId))
                    .OrderByDescending(m => m.CreateAt).ToList();
                return View("ProductCategory", product);
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult ProductBrand(string slug)
        {
            var brand = db.Brands.Where(m => m.Status == 1 && m.Slug == slug).FirstOrDefault();
            var product = db.Products.Where(m=>m.Status == 1 && m.BrandId == brand.Id)
                .OrderByDescending(m => m.CreateAt).ToList();
            return View("ProductBrand");
        }
        public ActionResult ProductDetail(Product product)
        {
            int catid = product.CatId;
            var listcatid = new List<int>(); //danh sach
            listcatid.Add(catid); //thêm phần tử vào danh sách
            var categorys = db.Categories.Where(m => m.ParentId == catid && m.Status == 1).ToList();
            if (categorys.Count > 0)
            {
                foreach (var cat in categorys)
                {
                    listcatid.Add(cat.Id);
                    var categorys1 = db.Categories.Where(m => m.ParentId == cat.Id && m.Status == 1).ToList();
                    if (categorys1.Count > 0)
                    {
                        foreach (var cat1 in categorys1)
                        {
                            listcatid.Add(cat1.Id);
                        }
                    }
                }
            }
            var product_other = db.Products.Where(m=>m.Status == 1 && m.Id != product.Id && listcatid.Contains(m.CatId))
                .OrderByDescending(m => m.CreateAt).Take(4).ToList();
            ViewBag.ListOther = product_other;
            return View("ProductDetail", product);
        }
        public ActionResult ProductHot(string slug)
        {
            return View("ProductHot" /*list*/);
        }
        //nhóm bài viết
        public ActionResult Post()
        {
            int pageNumber = page ?? 1;
            var model = new List<Post>();
            model = db.Posts.Where(m => m.Status == 1).OrderByDescending(m => m.CreateAt).Skip(6 * (pageNumber - 1)).Take(6).ToList();
            var totalPosts = db.Posts.Where(m => m.Status == 1).Count();
            ViewBag.TotalPage = (totalPosts / 6) + (totalPosts % 6 == 0 ? 0 : 1);
            return View("Post", model);
        }
        public ActionResult PostTopic(string slug)
        {
            var topic = db.Topics.Where(m => m.Status == 1 && m.Slug == slug).FirstOrDefault();
            var posts = db.Posts.Where(m => m.Status == 1 && m.PostType == "post" && m.TopicId == topic.Id)
                .OrderByDescending(m => m.CreateAt).ToList();
            return View("PostTopic", posts);
        }
        public ActionResult PostPage(string slug)
        {
            var page = db.Posts.Where(m => m.Status == 1 && m.PostType == "page" && m.Slug == slug)
               .FirstOrDefault();
            return View("PostPage", page);
        }
        public ActionResult PostDetail(Post post)
        {
            var post_other = db.Posts.Where(m => m.Status == 1 && m.Id != post.Id && m.TopicId == post.TopicId &&
            m.PostType == "post").OrderByDescending(m => m.CreateAt).Take(6).ToList();
            ViewBag.Post_other = post_other;
            return View("PostDetail", post);
        }
        public ActionResult Contact(string page)
        {
            return View("Contact", page);
        }
        public ActionResult Dangnhap(string page)
        {
            return View("Dangnhap", page);
        }
        
        public ActionResult Introduce(string page)
        {
            return View("Introduce", page);
        }
        public ActionResult PageBuy(string page)
        {
            return View("PageBuy", page);
        }
        
        //hàm lỗi
        public ActionResult Error404(string slug)
        {
            return View("Error404");
        }
    }
}