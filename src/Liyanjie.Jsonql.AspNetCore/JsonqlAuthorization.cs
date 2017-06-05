using Liyanjie.Jsonql.Core;
using Microsoft.AspNetCore.Http;

namespace Liyanjie.Jsonql.AspNetCore
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
