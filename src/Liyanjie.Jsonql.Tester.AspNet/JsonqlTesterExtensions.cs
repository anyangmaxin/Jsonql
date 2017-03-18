using Liyanjie.Jsonql.Tester;
using Liyanjie.Jsonql.Tester.AspNet;

namespace System.Web
{
    /// <summary>
    /// 
    /// </summary>
    public static class JsonqlTesterExtensions
    {
        /// <summary>
        /// Jsonql
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configue"></param>
        /// <param name="routeBase"></param>
        /// <returns></returns>
        public static HttpApplication UseJsonqlTester(this HttpApplication app, Action<JsonqlTesterOptions> configue = null, string routeBase = "jsonql-tester")
        {
            var jsonqlTesterOptions = new JsonqlTesterOptions();
            configue?.Invoke(jsonqlTesterOptions);

            if (new JsonqlTesterSchemaMIddleware(JsonqlExtensions.JsonOptions, jsonqlTesterOptions, $"{routeBase}/schema.json").Invoke(app.Context))
                app.Context.Response.End();

            if (new JsonqlTesterRedirectMiddleware(routeBase, $"{routeBase}/index.html").Invoke(app.Context))
                app.Context.Response.End();

            if (new JsonqlTesterEmbeddedMIddleware(typeof(Embedded).Assembly, Embedded.FileNamespace, $"/{routeBase}/").Invoke(app.Context))
                app.Context.Response.End();

            return app;
        }
    }
}