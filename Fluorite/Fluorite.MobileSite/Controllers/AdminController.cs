using System;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using Fluorite.MobileSite.Data;
using Fluorite.MobileSite.Models;

namespace Fluorite.MobileSite.Controllers
{
    [CustomAuthorize]
    [WebHandleErrorAttribute]
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult Seller()
        {
            var list = new DB().Sellers.OrderByDescending(x => x.CreateTime).ToList();
            ViewBag.Sellers = list;
            return View();
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
        [HttpGet]
        public ActionResult AddArticle(string sellerId)
        {
            ViewBag.SellerId = sellerId;
            return View();
        }
        [HttpGet]
        public ActionResult EditArticle(string articleId)
        {
            return View("AddArticle");
        }

        [HttpPost]
        public ActionResult AddArticle([ModelBinder(typeof(JsonBinder<AddArticleUICommand>))]AddArticleUICommand command)
        {
            using (var db = new DB())
            {
                using (var transaction = new TransactionScope())
                {
                    db.Articles.Add(new Article()
                    {
                        Id=Guid.NewGuid(),
                        Content = command.Details,
                        CreateTime = DateTime.Now,
                        SellerId = new Guid(command.SellerId),
                        Title = command.Title,
                        Valid = true,
                        Type = (ArticleType)command.Type
                    });
                    db.SaveChanges();
                    transaction.Complete();
                }
            }

            return new HttpStatusCodeResult(200);
        }
        [HttpGet]
        public ActionResult Articles(string sellerId)
        {
            var guid=new Guid(sellerId);
            var list = new DB().Articles.Where(x=>x.SellerId==guid).OrderByDescending(x => x.CreateTime).ToList();
            ViewBag.Articles = list;
            ViewBag.SellerId = sellerId;
            return View();
        }

        [HttpPost]
        public ActionResult SaveImage(string sellerId)
        {
            var httpPostedFile = Request.Files[0];
            var imageName = Guid.NewGuid() + ".jpg";
            var virtualPath ="~/temp/" + imageName;
            var fullPath = Server.MapPath(virtualPath);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            httpPostedFile.SaveAs(fullPath);
            return Content(imageName);
        }

        

        public class AddArticleUICommand 
        {
            public string Details { get; set; }
            public string SellerId { get; set; }
            public int Type { get; set; }
            public string Title { get; set; }
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