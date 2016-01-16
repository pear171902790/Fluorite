using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fluorite.MobileSite.Data;

namespace Fluorite.MobileSite.Models
{
    public static class Extend
    {
        public static bool NullOrNone<T>(this List<T> list)
        {
            return list == null || (!list.Any());
        }

        public static bool Exist<T>(this List<T> list,int i)
        {
            return list.Count > i;
        }

        public static string DisplayName(this ArticleType type)
        {
            if (type == ArticleType.Carousel)
                return "轮播大图";
            if (type == ArticleType.Common)
                return "页面中部";
            if (type == ArticleType.Menu)
                return "左侧菜单";

            return "页面底部";
        }
    }
}