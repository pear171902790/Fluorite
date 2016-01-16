using System.Web.Mvc;
using System.Web.Security;
using Fluorite.MobileSite.Models;

namespace Fluorite.MobileSite.Controllers
{
    [WebHandleErrorAttribute]
    public class AuthenticationController : Controller
    {
        public ActionResult Logon()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckAuthentication([ModelBinder(typeof(JsonBinder<LogonUICommand>))]LogonUICommand logonUiCommand)
        {
            if (logonUiCommand.Username != "bjabm" || logonUiCommand.Password != "testSite123")
            {
                throw new LogicException();
            }
            CookieHelper.SetAuthCookie("bjabm", false);
            return new HttpStatusCodeResult(200);
        }
        public ActionResult SignOut()
        {
            CookieHelper.SignOut();
            return new RedirectResult(FormsAuthentication.LoginUrl);
        }
    }
    public class LogonUICommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    
}