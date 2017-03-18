using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using System;
using System.Threading.Tasks;

namespace Liyanjie.Jsonql.Tester.AspNetCore
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonqlTesterRedirectMiddleware
    {
        readonly RequestDelegate next;
        readonly string from;
        readonly string to;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public JsonqlTesterRedirectMiddleware(
            RequestDelegate next,
            string from,
            string to)
        {
            this.next = next;
            this.from = from;
            this.to = to;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            if (matchRequesting(httpContext.Request))
            {
                respondRedirect(httpContext.Response, httpContext.Request.PathBase);
                return;
            }

            await next(httpContext);
        }

        bool matchRequesting(HttpRequest request)
        {
            if ("GET".Equals(request.Method, StringComparison.OrdinalIgnoreCase))
            {
                var routeValueDictionary = new RouteValueDictionary();
                var templateMatcher = new TemplateMatcher(TemplateParser.Parse(from), routeValueDictionary);
                return templateMatcher.TryMatch(request.Path, routeValueDictionary);
            }

            return false;
        }

        void respondRedirect(HttpResponse response, string pathBase)
        {
            response.Redirect(pathBase + "/" + to);
        }
    }
}
