using Liyanjie.Jsonql.AspNet;

namespace System.Web
{
    /// <summary>
    /// 
    /// </summary>
    public static class JsonqlExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static JsonqlOptions JsonOptions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="optionsConfigure"></param>
        public static HttpApplication AddJsonql(this HttpApplication app, Action<JsonqlOptions> optionsConfigure)
        {
            JsonOptions = new JsonqlOptions();
            optionsConfigure?.Invoke(JsonOptions);

            return app;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="routeTemplate"></param>
        public static HttpApplication UseJsonql(this HttpApplication app, string routeTemplate = "jsonql")
        {
            if (new JsonqlMiddleware(JsonOptions, routeTemplate).Invoke(app.Context))
                app.Context.Response.End();

            return app;
        }
    }
}
