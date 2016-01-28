using System;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using Fluorite.MobileSite.Data;
using Fluorite.MobileSite.Models;

namespace Fluorite.MobileSite.Controllers
{
    [CustomAuthorize]
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult Seller()
        {
            var list = new DB().Sellers.Where(x => x.Valid).OrderByDescending(x => x.CreateTime).ToList();
            ViewBag.Host = Request.Url.Authority;
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
            var logoUrl = string.Empty;
            if (!string.IsNullOrEmpty(sellerUiCommand.ImageName))
            {
                string sourceFile = Server.MapPath("~/temp/" + sellerUiCommand.ImageName);
                var sellerFolderName = sellerUiCommand.Id.Replace("-", String.Empty);
                string destinationPath = Server.MapPath("~/Content/" + sellerFolderName + "/");
                if (!System.IO.Directory.Exists(destinationPath))
                {
                    System.IO.Directory.CreateDirectory(destinationPath);
                }
                string destinationFile = System.IO.Path.Combine(destinationPath, sellerUiCommand.ImageName);
                System.IO.File.Move(sourceFile, destinationFile);
                logoUrl = "/Content/" + sellerFolderName + "/" + sellerUiCommand.ImageName;
            }
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
                    seller.LogoUrl = logoUrl;
                    seller.LogoText = sellerUiCommand.LogoText;
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
            ViewBag.Article = new DB().Articles.SingleOrDefault(x => x.Id == new Guid(articleId));
            return View();
        }

        [HttpPost]
        public ActionResult EditArticle([ModelBinder(typeof(JsonBinder<EditArticleUICommand>))]EditArticleUICommand command)
        {
            var coverUrl = string.Empty;
            if (!string.IsNullOrEmpty(command.ImageName))
            {
                string sourceFile = Server.MapPath("~/temp/" + command.ImageName);
                var sellerFolderName = command.SellerId.Replace("-", String.Empty);
                string destinationPath = Server.MapPath("~/Content/" + sellerFolderName + "/");
                if (!System.IO.Directory.Exists(destinationPath))
                {
                    System.IO.Directory.CreateDirectory(destinationPath);
                }
                string destinationFile = System.IO.Path.Combine(destinationPath, command.ImageName);
                System.IO.File.Move(sourceFile, destinationFile);
                coverUrl = "/Content/" + sellerFolderName + "/" + command.ImageName;
            }
            using (var db = new DB())
            {
                using (var transaction = new TransactionScope())
                {
                    var article = db.Articles.SingleOrDefault(x => x.Id == new Guid(command.Id));
                    article.Content = command.Details;
                    article.Order = command.Order;
                    article.Title = command.Title;
                    article.Type = (ArticleType) command.Type;
                    article.CoverUrl = string.IsNullOrEmpty(coverUrl) ? article.CoverUrl : coverUrl;
                    article.ExternalUrl = command.ExternalUrl;
                    article.IsExternal = command.IsExternal;
                    article.Remarks = command.Remarks;
                    db.SaveChanges();
                    transaction.Complete();
                }
            }
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult AddArticle([ModelBinder(typeof(JsonBinder<AddArticleUICommand>))]AddArticleUICommand command)
        {
            var coverUrl = string.Empty;
            if (!string.IsNullOrEmpty(command.ImageName))
            {
                string sourceFile = Server.MapPath("~/temp/" + command.ImageName);
                var sellerFolderName = command.SellerId.Replace("-", String.Empty);
                string destinationPath = Server.MapPath("~/Content/" + sellerFolderName + "/");
                if (!System.IO.Directory.Exists(destinationPath))
                {
                    System.IO.Directory.CreateDirectory(destinationPath);
                }
                string destinationFile = System.IO.Path.Combine(destinationPath, command.ImageName);
                System.IO.File.Move(sourceFile, destinationFile);
                coverUrl = "/Content/" + sellerFolderName + "/" + command.ImageName;
            }
            using (var db = new DB())
            {
                using (var transaction = new TransactionScope())
                {
                    db.Articles.Add(new Article()
                    {
                        Id = Guid.NewGuid(),
                        Content = command.Details,
                        Order = command.Order,
                        CreateTime = DateTime.Now,
                        SellerId = new Guid(command.SellerId),
                        Title = command.Title,
                        Valid = true,
                        Type = (ArticleType)command.Type,
                        CoverUrl = string.IsNullOrEmpty(coverUrl) ? "/Content/nopic.jpg" : coverUrl,
                        ExternalUrl = command.ExternalUrl,
                        IsExternal = command.IsExternal,
                        Remarks = command.Remarks
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
            var guid = new Guid(sellerId);
            var seller = new DB().Sellers.SingleOrDefault(x => x.Id == guid);
            ViewBag.Seller = seller;
            return View();
        }

        [HttpPost]
        public ActionResult SaveImage()
        {
            var httpPostedFile = Request.Files[0];
            var imageName = Guid.NewGuid() + ".jpg";
            var virtualPath = "~/temp/" + imageName;
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
            public bool IsExternal { get; set; }
            public string Remarks { get; set; }
            public string Details { get; set; }
            public string SellerId { get; set; }
            public int Type { get; set; }
            public string Title { get; set; }
            public string ExternalUrl { get; set; }
            public string ImageName { get; set; }
            public string Order { get; set; }
        }

        public class EditArticleUICommand
        {
            public bool IsExternal { get; set; }
            public string Remarks { get; set; }
            public string Details { get; set; }
            public string SellerId { get; set; }
            public int Type { get; set; }
            public string Title { get; set; }
            public string ExternalUrl { get; set; }
            public string ImageName { get; set; }
            public string Id { get; set; }
            public string Order { get; set; }
        }
        public class SellerUICommand
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Contacts { get; set; }
            public string Tel { get; set; }
            public string Remarks { get; set; }
            public string ImageName { get; set; }
            public string LogoText { get; set; }
        }

        public ActionResult DeleteSeller(string id)
        {
            using (var db = new DB())
            {
                using (var transaction = new TransactionScope())
                {
                    var seller = db.Sellers.SingleOrDefault(x => x.Id == new Guid(id));
                    seller.Valid = false;
                    db.SaveChanges();
                    transaction.Complete();
                }
            }
            return new HttpStatusCodeResult(200);
        }

        public ActionResult DeleteArticle(string id)
        {
            using (var db = new DB())
            {
                using (var transaction = new TransactionScope())
                {
                    var article = db.Articles.SingleOrDefault(x => x.Id == new Guid(id));
                    article.Valid = false;
                    db.SaveChanges();
                    transaction.Complete();
                }
            }
            return new HttpStatusCodeResult(200);
        }
    }
}