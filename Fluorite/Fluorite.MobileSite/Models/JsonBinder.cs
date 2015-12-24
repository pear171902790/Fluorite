using System.IO;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Fluorite.MobileSite.Models
{
    public class JsonBinder<T> : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            var reader = new StreamReader(request.InputStream);
            string json = reader.ReadToEnd();
            if (string.IsNullOrEmpty(json))
                return json;
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
