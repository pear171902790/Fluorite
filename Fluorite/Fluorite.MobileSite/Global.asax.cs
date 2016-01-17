using System.Data.Entity;
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
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DB, Migrations.Configuration>());
            var list = new DB().Users.Take(1).ToList();
        }
        protected void Application_EndRequest()
        {
            var statusCode = Context.Response.StatusCode;
            if (statusCode == 404)
            {
                Response.Clear();
                Response.RedirectToRoute("NotFound");
            }
        }
    }
}
