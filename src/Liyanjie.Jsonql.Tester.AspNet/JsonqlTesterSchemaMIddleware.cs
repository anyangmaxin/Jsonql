using Liyanjie.TemplateMatching;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Web;
using System.Web.Routing;

namespace Liyanjie.Jsonql.Tester.AspNet
{
    internal class JsonqlTesterSchemaMIddleware
    {
        readonly JsonqlOptions jsonqlOptions;
        readonly JsonqlTesterOptions jsonqlTesterOptions;
        readonly string jsonSchemaPath;

        public JsonqlTesterSchemaMIddleware(
            JsonqlOptions jsonqlOptions,
            JsonqlTesterOptions jsonqlTesterOptions,
            string jsonSchemaPath)
        {
            this.jsonqlOptions = jsonqlOptions;
            this.jsonqlTesterOptions = jsonqlTesterOptions;
            this.jsonSchemaPath = jsonSchemaPath;
        }

        public bool Invoke(HttpContext httpContext)
        {
            if (matchRequesting(httpContext.Request))
            {
                var schema = new Liyanjie.Jsonql.Explorer.Generator(jsonqlOptions.Resources).Generate();
                respondSchema(httpContext.Response, JsonConvert.SerializeObject(new
                {
                    jsonqlTesterOptions?.Title,
                    jsonqlTesterOptions?.Description,
                    jsonqlTesterOptions?.ServerUrl,
                    schema.ResourceInfos,
                    schema.ResourceTypes,
                    schema.ResourceMethods,
                }));
                return true;
            }

            return false;
        }

        bool matchRequesting(HttpRequest request)
        {
            if ("GET".Equals(request.HttpMethod, StringComparison.OrdinalIgnoreCase))
            {
                var routeValues = new RouteValueDictionary();
                var templateMatcher = new TemplateMatcher(TemplateParser.Parse(jsonSchemaPath), routeValues);
                return templateMatcher.TryMatch(request.Path, routeValues);
            }

            return false;
        }

        void respondSchema(HttpResponse response, string content)
        {
            response.StatusCode = 200;
            response.ContentType = "application/json";
            using (var writter = new StreamWriter(response.OutputStream))
            {
                writter.Write(content);
            }
        }
    }
}
