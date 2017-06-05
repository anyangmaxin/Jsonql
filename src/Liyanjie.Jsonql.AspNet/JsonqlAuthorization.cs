using Liyanjie.Jsonql.Core;
using System.Web;

namespace Liyanjie.Jsonql.AspNet
{
    internal class JsonqlAuthorization : IJsonqlAuthorization
    {
        public JsonqlAuthorization(HttpContext httpContext)
        {
            this.httpContext = httpContext;
        }

        readonly HttpContext httpContext;

        public HttpContext HttpContext => httpContext;
    }
}
