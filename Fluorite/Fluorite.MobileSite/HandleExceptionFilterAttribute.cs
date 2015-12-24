using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Fluorite.MobileSite
{
    public class HandleExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.NotFound);
            base.OnException(actionExecutedContext);
        }
    }
}
