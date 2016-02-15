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

        public ActionResult Seller(string sellerName)
        {
            using (var db = new DB())
            {
                var seller = db.Sellers.FirstOrDefault(x => x.Name == sellerName);
                if (seller == null)
                {
                    throw new Exception();
                }
                ViewBag.Title = seller.Name;
                ViewBag.Seller = seller;
                ViewBag.Crousels =
                    seller.Articles.Where(x => x.Type == ArticleType.Carousel&&x.Valid).Take(5).ToList();
                ViewBag.Commons =
                    seller.Articles.Where(x => x.Type == ArticleType.Common && x.Valid).Take(4).ToList();
                ViewBag.Menus =
                    seller.Articles.Where(x => x.Type == ArticleType.Menu && x.Valid).Take(7).ToList();
                ViewBag.Smalls =
                    seller.Articles.Where(x => x.Type == ArticleType.SmallCover && x.Valid)
                        .Take(2)
                        .ToList();
            }
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult Article(string articleId)
        {
            Seller seller;
            Article article;
            using (var db = new DB())
            {
                article = db.Articles.SingleOrDefault(x => x.Id == new Guid(articleId));
                seller = db.Sellers.SingleOrDefault(x => x.Id == article.SellerId);
            }
            if (!string.IsNullOrEmpty(article.ExternalUrl))
            {
                return new RedirectResult(article.ExternalUrl);
            }
            ViewBag.Seller = seller;
            return View(article);
        }

        public ActionResult Self()
        {
            List<Self> selves;
            using (var db = new DB())
            {
                selves = db.Selves.ToList();
            }
            if (!selves.Any())
            {
                throw new Exception();
            }
            return View(model:selves[0].Content);
        }


        public ActionResult Order(string sName,string pName)
        {
            Article article;
            try
            {
                using (var db = new DB())
                {
                    var seller = db.Sellers.SingleOrDefault(x => x.Name == sName);
                    article = db.Articles.SingleOrDefault(x => x.Title == pName && x.SellerId == seller.Id);
                }
            }
            catch (Exception)
            {
                article = new Article()
                {
                    OrderTitle = "标题标题标题标题",
                    Order = "内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容内容",
                    OrderPicUrl = "/页面内元素打包/内容元素/大图01.jpg"
                };
                
            }
            return View(article);
        }
    }
}