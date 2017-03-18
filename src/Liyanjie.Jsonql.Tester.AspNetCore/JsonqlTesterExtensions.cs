using Liyanjie.Jsonql.Tester;
using Liyanjie.Jsonql.Tester.AspNetCore;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using System;
using System.Reflection;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 
    /// </summary>
    public static class JsonqlTesterExtensions
    {
        /// <summary>
        /// Jsonql
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <param name="configue"></param>
        /// <param name="routeTemplate"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseJsonqlTester(this IApplicationBuilder applicationBuilder, Action<JsonqlTesterOptions> configue = null, string routeTemplate = "jsonql-tester")
        {
            var jsonqlTesterOptions = new JsonqlTesterOptions();
            configue?.Invoke(jsonqlTesterOptions);
            applicationBuilder.UseMiddleware<JsonqlTesterSchemaMIddleware>(jsonqlTesterOptions, $"{routeTemplate}/schema.json");

            applicationBuilder.UseMiddleware<JsonqlTesterRedirectMiddleware>(routeTemplate, $"{routeTemplate}/index.html");

            var fileServerOptions = new FileServerOptions
            {
                RequestPath = $"/{routeTemplate}",
                EnableDefaultFiles = false,
                FileProvider = new EmbeddedFileProvider(typeof(Embedded).GetTypeInfo().Assembly, Embedded.FileNamespace),
            };
            fileServerOptions.StaticFileOptions.ContentTypeProvider = new FileExtensionContentTypeProvider();
            applicationBuilder.UseFileServer(fileServerOptions);

            return applicationBuilder;
        }
    }
}