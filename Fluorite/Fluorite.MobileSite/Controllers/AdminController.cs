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

        [HttpPost]
        public ActionResult EditSeller([ModelBinder(typeof(JsonBinder<SellerUICommand>))]SellerUICommand sellerUiCommand)
        {
            using (var db = new DB())
            {
                using (var transaction = new TransactionScope())
                {
                    var seller = db.Sellers.SingleOrDefault(x => x.Id == new Guid(sellerUiCommand.Id));
                    seller.Name = sellerUiCommand.Name;
                    seller.Contacts = sellerUiCommand.Contacts;
                    seller.CreateTime = DateTime.Now;
                    seller.Remarks = sellerUiCommand.Remarks;
                    seller.Tel = sellerUiCommand.Tel;
                    db.SaveChanges();
                    transaction.Complete();
                }
            }
            return new HttpStatusCodeResult(200);
        }
        [HttpGet]
        public ActionResult AddArticle(string sellerId)
        {
            ViewBag.Seller = new DB().Sellers.SingleOrDefault(x => x.Id == new Guid(sellerId));
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
            var seller = new DB().Sellers.SingleOrDefault(x => x.Id == guid);
            ViewBag.Articles = seller.Articles.OrderByDescending(x => x.CreateTime).ToList();
            ViewBag.Seller = seller;
            return View();
        }

        [HttpPost]
        public ActionResult SaveImage()
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

            public string ImageName { get; set; }
        }
        public class SellerUICommand
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Contacts { get; set; }
            public string Tel { get; set; }
            public string Remarks { get; set; }
        }
    }
}