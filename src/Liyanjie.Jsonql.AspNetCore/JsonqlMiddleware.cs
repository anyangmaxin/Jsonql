using System;
using System.IO;
using System.Threading.Tasks;
using Liyanjie.Jsonql.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

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
            this.next = next ?? throw new ArgumentNullException(nameof(next));

            if (jsonqlOptions == null)
                throw new ArgumentNullException(nameof(jsonqlOptions));
            this.jsonqlOptions = jsonqlOptions.Value;

            this.routeTemplate = routeTemplate ?? throw new ArgumentNullException(nameof(routeTemplate));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            if (MatchRequesting(httpContext.Request, out string query))
            {
                if (!(jsonqlOptions.Authorize?.Invoke(httpContext) ?? true))
                    return;

                if (!string.IsNullOrEmpty(query))
                {
                    using (var queryHandler = new QueryHandler(jsonqlOptions.Resources, jsonqlOptions.JsonqlIncluder, jsonqlOptions.JsonqlLinqer, jsonqlOptions.JsonqlEvaluator))
                    {
                        try
                        {
                            var content = await queryHandler.Handle(query, new JsonqlAuthorization(httpContext));
                            await RespondDocument(httpContext.Response, content);
                        }
                        catch (Exception exception)
                        {
                            await RespondDocument(httpContext.Response, JsonConvert.SerializeObject(new { Message = exception.Message }), 500);
                        }
                    }
                }
                return;
            }

            await next(httpContext);
        }

        bool MatchRequesting(HttpRequest request, out string query)
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

        async Task RespondDocument(HttpResponse response, string content, int statusCode = 200)
        {
            response.StatusCode = statusCode;
            response.ContentType = "application/json";

            using (var writer = new StreamWriter(response.Body))
            {
                await writer.WriteAsync(content);
            }
        }
    }
}
