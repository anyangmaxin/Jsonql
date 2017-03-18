using Liyanjie.Jsonql.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Liyanjie.Jsonql.AspNetCore
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonqlMiddleware
    {
        readonly RequestDelegate next;
        readonly JsonqlOptions jsonqlOptions;
        readonly string routeTemplate;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="jsonqlOptions"></param>
        /// <param name="routeTemplate"></param>
        public JsonqlMiddleware(
            RequestDelegate next,
            IOptions<JsonqlOptions> jsonqlOptions,
            string routeTemplate)
        {
            if (next == null)
                throw new ArgumentNullException(nameof(next));
            this.next = next;

            if (jsonqlOptions == null)
                throw new ArgumentNullException(nameof(jsonqlOptions));
            this.jsonqlOptions = jsonqlOptions.Value;

            if (routeTemplate == null)
                throw new ArgumentNullException(nameof(routeTemplate));
            this.routeTemplate = routeTemplate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            string query;
            if (matchRequesting(httpContext.Request, out query))
            {
                if (!(jsonqlOptions.Authorize?.Invoke(httpContext) ?? true))
                {
                    httpContext.Response.Clear();
                    httpContext.Response.StatusCode = 403;
                    return;
                }

                if (!string.IsNullOrEmpty(query))
                {
                    using (var queryHandler = new QueryHandler(jsonqlOptions.Resources, jsonqlOptions.DynamicEvaluator, jsonqlOptions.DynamicLinq))
                    {
                        var content = await queryHandler.Handle(query, new Authorization(httpContext));
                        await respondDocument(httpContext.Response, content);
                    }
                }
                return;
            }

            await next(httpContext);
        }

        private bool matchRequesting(HttpRequest request, out string query)
        {
            query = null;

            if ("GET".Equals(request.Method, StringComparison.OrdinalIgnoreCase))
            {
                var routeValues = new RouteValueDictionary();
                var templateMatcher = new TemplateMatcher(TemplateParser.Parse(routeTemplate), routeValues);
                if (templateMatcher.TryMatch(request.Path, routeValues))
                {
                    query = jsonqlOptions.FindQuery?.Invoke(request);

                    if (query == null)
                        query = request.Query["query"];

                    if (query == null)
                        query = request.Headers["query"];

                    if (query == null)
                        using (var reader = new StreamReader(request.Body))
                            query = reader.ReadToEnd();

                    return true;
                }
            }

            return false;
        }

        private async Task respondDocument(HttpResponse response, string content)
        {
            response.StatusCode = 200;
            response.ContentType = "application/json";

            using (var writer = new StreamWriter(response.Body))
            {
                await writer.WriteAsync(content);
            }
        }
    }
}
