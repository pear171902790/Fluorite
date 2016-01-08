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
            ViewBag.Content = new DB().Articles.Where(x=>x.SellerId==guid).ToList();
            return View();
        }
    }
}