using System.Web.Http.Filters;

namespace Fluorite.MobileSite
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(HttpFilterCollection filters)
        {
            filters.Add(new HandleExceptionFilterAttribute());
        }
    }
}
