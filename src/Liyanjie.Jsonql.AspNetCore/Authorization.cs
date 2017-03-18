using Liyanjie.Jsonql.Core;
using Microsoft.AspNetCore.Http;

namespace Liyanjie.Jsonql.AspNetCore
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
