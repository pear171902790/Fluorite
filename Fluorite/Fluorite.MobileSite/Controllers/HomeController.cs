using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Fluorite.MobileSite.Data;

namespace Fluorite.MobileSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Seller(string sellerId)
        {
            var guid=new Guid(sellerId);
            ViewBag.Crousels = new DB().Articles.Where(x => x.SellerId == guid&&x.Type==ArticleType.Carousel).Take(5).ToList();
            ViewBag.Commons = new DB().Articles.Where(x => x.SellerId == guid && x.Type == ArticleType.Common).Take(4).ToList();
            ViewBag.Menus = new DB().Articles.Where(x => x.SellerId == guid && x.Type == ArticleType.Menu).Take(7).ToList();
            ViewBag.Smalls = new DB().Articles.Where(x => x.SellerId == guid && x.Type == ArticleType.SmallCover).Take(2).ToList();
            return View();
        }
    }
}