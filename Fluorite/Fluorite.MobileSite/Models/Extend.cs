using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}