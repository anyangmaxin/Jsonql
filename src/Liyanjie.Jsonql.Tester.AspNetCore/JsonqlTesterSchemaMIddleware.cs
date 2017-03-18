using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Liyanjie.Jsonql.Tester.AspNetCore
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonqlTesterSchemaMIddleware
    {
        readonly RequestDelegate next;
        readonly JsonqlOptions jsonqlOptions;
        readonly JsonqlTesterOptions jsonqlTesterOptions;
        readonly TemplateMatcher requestMatcher;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="jsonqlOptions"></param>
        /// <param name="jsonqlTesterOptions"></param>
        /// <param name="jsonSchemaPath"></param>
        public JsonqlTesterSchemaMIddleware(
            RequestDelegate next,
            IOptions<JsonqlOptions> jsonqlOptions,
            JsonqlTesterOptions jsonqlTesterOptions,
            string jsonSchemaPath)
        {
            this.next = next;
            this.jsonqlOptions = jsonqlOptions.Value;
            this.jsonqlTesterOptions = jsonqlTesterOptions;
            this.requestMatcher = new TemplateMatcher(TemplateParser.Parse(jsonSchemaPath), new RouteValueDictionary());
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
                var schema = new Liyanjie.Jsonql.Explorer.Generator(jsonqlOptions.Resources).Generate();
                await respondSchema(httpContext.Response, JsonConvert.SerializeObject(new
                {
                    jsonqlTesterOptions?.Title,
                    jsonqlTesterOptions?.Description,
                    jsonqlTesterOptions?.ServerUrl,
                    schema.ResourceInfos,
                    schema.ResourceTypes,
                    schema.ResourceMethods,
                }));
                return;
            }

            await next(httpContext);
        }

        bool matchRequesting(HttpRequest request)
        {
            return ("GET".Equals(request.Method, StringComparison.OrdinalIgnoreCase)) && requestMatcher.TryMatch(request.Path, new RouteValueDictionary());
        }

        async Task respondSchema(HttpResponse response, string content)
        {
            response.StatusCode = 200;
            response.ContentType = "application/json";
            using (var writter = new StreamWriter(response.Body))
            {
                await writter.WriteAsync(content);
            }
        }
    }
}
