using System;
using System.Linq;
using System.Web.Mvc;
using Fluorite.MobileSite.Data;
using Fluorite.MobileSite.Models;

namespace Fluorite.MobileSite.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult Seller()
        {
            var list = DB.Instance.Sellers.OrderByDescending(x=>x.CreateTime).ToList();
            ViewBag.Sellers = list;
            return View();
        }
        [HttpGet]
        public ActionResult Logon()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckAuthentication([ModelBinder(typeof(JsonBinder<LogonUICommand>))]LogonUICommand logonUiCommand)
        {
            if (logonUiCommand.Username != "bjabm" || logonUiCommand.Password != "testSite123")
            {
                throw new Exception();
            }
            return new HttpStatusCodeResult(200);
        }

        public class LogonUICommand
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}