using Liyanjie.TemplateMatching;
using System;
using System.Web;
using System.Web.Routing;

namespace Liyanjie.Jsonql.Tester.AspNet
{
    internal class JsonqlTesterRedirectMiddleware
    {
        readonly string from;
        readonly string to;

        public JsonqlTesterRedirectMiddleware(
            string from,
            string to)
        {
            this.from = from;
            this.to = to;
        }

        public bool Invoke(HttpContext httpContext)
        {
            if (matchRequesting(httpContext.Request))
            {
                respondRedirect(httpContext.Response, "~");
                return true;
            }

            return false;
        }

        private bool matchRequesting(HttpRequest request)
        {
            if ("GET".Equals(request.HttpMethod, StringComparison.OrdinalIgnoreCase))
            {
                var routeValues = new RouteValueDictionary();
                var templateMatcher = new TemplateMatcher(TemplateParser.Parse(from), routeValues);
                return templateMatcher.TryMatch(request.Path, routeValues);
            }

            return false;
        }

        private void respondRedirect(HttpResponse response, string pathBase)
        {
            response.Redirect(pathBase + "/" + to);
        }
    }
}
