using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Fluorite.MobileSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");


            routes.MapRoute(
                name: "Self",
                url: "self",
                defaults: new { controller = "Home", action = "Self" }
            );

            routes.MapRoute(
                name: "Order",
                url: "order/{sName}/{pName}",
                defaults: new { controller = "Home", action = "Order", sName = UrlParameter.Optional, pName = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "NotFound",
                url: "404",
                defaults: new { controller = "Home", action = "NotFound", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Seller",
                url: "shop/{sellerName}",
                defaults: new { controller = "Home", action = "Seller" }
            );

            routes.MapRoute(
                name: "Admin",
                url: "gl",
                defaults: new { controller = "Admin", action = "Seller", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            
        }
    }
}
