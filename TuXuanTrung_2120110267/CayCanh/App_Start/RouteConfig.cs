using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CayCanh
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
              name: "Trang-chu",
              url: "trang-chu",
              defaults: new { controller = "Site", action = "Home", id = UrlParameter.Optional }
          );
            routes.MapRoute(
               name: "AllSanPham",
               url: "san-pham",
               defaults: new { controller = "Site", action = "Product", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "SanPhamTheoLoai",
              url: "san-pham-theo-loai",
              defaults: new { controller = "Site", action = "ProductCategory", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "BaiViet",
              url: "bai-viet",
              defaults: new { controller = "Site", action = "Post", id = UrlParameter.Optional }
          );
            
            routes.MapRoute(
              name: "LienHe",
              url: "lien-he",
              defaults: new { controller = "Site", action = "Contact", id = UrlParameter.Optional }
          );
            routes.MapRoute(
               name: "GioiThieu",
               url: "gioi-thieu",
               defaults: new { controller = "Site", action = "Introduce", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "GioHang",
               url: "gio-hang",
               defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
              name: "ThanhToan",
              url: "thanh-toan",
              defaults: new { controller = "Cart", action = "Thanhtoan", id = UrlParameter.Optional }
          );
            routes.MapRoute(
               name: "TimKiem",
               url: "tim-kiem",
               defaults: new { controller = "Search", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "DangNhap",
               url: "dang-nhap",
               defaults: new { controller = "Site", action = "Dangnhap", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "ChinhSachBanHang",
               url: "chinh-sach-ban-hang",
               defaults: new { controller = "Site", action = "PageBuy", id = UrlParameter.Optional }
           );
            
            //Khai báo URL động -nằm kế bên Default
            //routes.MapRoute(
            //    name: "SiteSlug",
            //    url: "{slug}",
            //    defaults: new { controller = "Site", action = "Home", id = UrlParameter.Optional }
            //);
            //  routes.MapRoute(
            //    name: "ChiTietSanPham",
            //    url: "{slug}",
            //    defaults: new { controller = "Site", action = "ProductDetail", id = UrlParameter.Optional }
            //);
            routes.MapRoute(
              name: "ChiTietBaiViet",
              url: "{slug}",
              defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Site", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}
