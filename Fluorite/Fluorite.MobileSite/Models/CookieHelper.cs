using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Fluorite.MobileSite.Models
{
    public class CookieHelper
    {
        public static long GetUserId(HttpRequestBase request)
        {
            return GetId(request);
        }

        private static long GetId(HttpRequestBase request)
        {
            HttpCookie userCookie = request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(userCookie.Value);
            return Convert.ToInt64(ticket.Name);
        }

        public static void SetAuthCookie(string id, bool isRememberMe)
        {
            FormsAuthentication.SetAuthCookie(id, isRememberMe);
        }

        public static void SignOut()
        {
            FormsAuthentication.SignOut();
        }

    }
}