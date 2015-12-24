using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Fluorite.MobileSite.Data;

namespace Fluorite.MobileSite
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalConfiguration.Configuration.Filters);
            var list = DB.Instance.Users.Take(1).ToList();
        }
    }
}
