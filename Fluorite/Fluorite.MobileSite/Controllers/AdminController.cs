using System;
using System.Linq;
using System.Transactions;
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
            var list = new DB().Sellers.OrderByDescending(x => x.CreateTime).ToList();
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

        [HttpPost]
        public ActionResult AddSeller([ModelBinder(typeof(JsonBinder<SellerUICommand>))]SellerUICommand sellerUiCommand)
        {
            var seller = new Seller()
            {
                Name = sellerUiCommand.Name,
                Contacts = sellerUiCommand.Contacts,
                Id = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                Remarks = sellerUiCommand.Remarks,
                Tel = sellerUiCommand.Tel,
                Valid = true
            };
            using (var db = new DB())
            {
                using (var transaction = new TransactionScope())
                {
                    db.Sellers.Add(seller);
                    db.SaveChanges();
                    transaction.Complete();
                }
            }
            return new HttpStatusCodeResult(200);
        }

        public ActionResult AddArticle()
        {
            return View();
        }

        public class LogonUICommand
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
        public class SellerUICommand
        {
            public string Name { get; set; }
            public string Contacts { get; set; }
            public string Tel { get; set; }
            public string Remarks { get; set; }
        }
    }
}