﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Fluorite.MobileSite.Data;

namespace Fluorite.MobileSite.Controllers
{
    [WebHandleErrorAttribute]
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
                ViewBag.Crousels =
                    seller.Articles.Where(x => x.Type == ArticleType.Carousel).Take(5).ToList();
                ViewBag.Commons =
                    seller.Articles.Where(x => x.Type == ArticleType.Common).Take(4).ToList();
                ViewBag.Menus =
                    seller.Articles.Where(x => x.Type == ArticleType.Menu).Take(7).ToList();
                ViewBag.Smalls =
                    seller.Articles.Where(x => x.Type == ArticleType.SmallCover)
                        .Take(2)
                        .ToList();
            }
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}