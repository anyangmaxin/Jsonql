using System;
using System.IO;
using System.Web;
using System.Web.Routing;
using Liyanjie.Jsonql.Core;
using Liyanjie.TemplateMatching;
using Newtonsoft.Json;

namespace Liyanjie.Jsonql.AspNet
{
    internal class JsonqlMiddleware
    {
        readonly JsonqlOptions jsonqlOptions;
        readonly string routeTemplate;

        public JsonqlMiddleware(
            JsonqlOptions jsonqlOptions,
            string routeTemplate)
        {
            this.jsonqlOptions = jsonqlOptions ?? throw new ArgumentNullException(nameof(jsonqlOptions));
            this.routeTemplate = routeTemplate ?? throw new ArgumentNullException(nameof(routeTemplate));
        }

        public bool Invoke(HttpContext httpContext)
        {
            string query;
            if (MatchRequesting(httpContext.Request, out query))
            {
                if (!(jsonqlOptions.Authorize?.Invoke(httpContext) ?? true))
                {
                    httpContext.Response.Clear();
                    httpContext.Response.StatusCode = 403;
                    return true;
                }

                if (!string.IsNullOrEmpty(query))
                {
                    using (var queryHandler = new QueryHandler(jsonqlOptions.Resources, jsonqlOptions.JsonqlIncluder, jsonqlOptions.JsonqlLinqer, jsonqlOptions.JsonqlEvaluator))
                    {
                        try
                        {
                            var t = queryHandler.Handle(query, new JsonqlAuthorization(httpContext));
                            t.Wait();
                            var content = t.Result;
                            RespondDocument(httpContext.Response, content);
                        }
                        catch (Exception exception)
                        {
                            RespondDocument(httpContext.Response, JsonConvert.SerializeObject(exception), 500);
                        }
                    }
                }
                return true;
            }

            return false;
        }

        bool MatchRequesting(HttpRequest request, out string query)
        {
            query = null;

            if ("GET".Equals(request.HttpMethod, StringComparison.OrdinalIgnoreCase))
            {
                var routeValues = new RouteValueDictionary();
                var templateMatcher = new TemplateMatcher(TemplateParser.Parse(routeTemplate), routeValues);
                if (templateMatcher.TryMatch(request.Path, routeValues))
                {
                    query = jsonqlOptions.FindQuery?.Invoke(request);

                    if (query == null)
                        query = request.QueryString["query"];

                    if (query == null)
                        query = request.Headers["query"];

                    if (query == null)
                        using (var reader = new StreamReader(request.InputStream))
                            query = reader.ReadToEnd();

                    return true;
                }
            }

            return false;
        }

        void RespondDocument(HttpResponse response, string content, int statusCode = 200)
        {
            response.StatusCode = statusCode;
            response.ContentType = "application/json";

            using (var writer = new StreamWriter(response.OutputStream))
            {
                writer.Write(content);
            }
        }
    }
}
