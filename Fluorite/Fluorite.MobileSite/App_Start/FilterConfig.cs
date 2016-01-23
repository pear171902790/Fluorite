using System;
using System.Security.Policy;
using System.Web.Mvc;
using FilterAttribute = System.Web.Mvc.FilterAttribute;
using IExceptionFilter = System.Web.Mvc.IExceptionFilter;

namespace Fluorite.MobileSite
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new WebHandleErrorAttribute());
        }
    }
    public class WebHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            if (filterContext.Exception is LogicException)
            {
                filterContext.Result = new HttpStatusCodeResult(500);
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult("NotFound", null);
            }
        }
    }
    public class LogicException : Exception
    {

    }
}
