using Liyanjie.Jsonql.Core;
using System.Web;

namespace Liyanjie.Jsonql.AspNet
{
    internal class Authorization : IAuthorization
    {
        public Authorization(HttpContext httpContext)
        {
            this.httpContext = httpContext;
        }

        readonly HttpContext httpContext;

        public HttpContext HttpContext => httpContext;
    }
}
