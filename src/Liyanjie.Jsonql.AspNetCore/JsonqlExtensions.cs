using Liyanjie.Jsonql.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 
    /// </summary>
    public static class JsonqlExtensions
    {
        /// <summary>
        /// Jsonql
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configureOptions"></param>
        /// <returns></returns>
        public static IServiceCollection AddJsonql(this IServiceCollection services, Action<JsonqlOptions> configureOptions)
        {
            return services.Configure(configureOptions);
        }

        /// <summary>
        /// Jsonql
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <param name="routeTemplate"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseJsonql(this IApplicationBuilder applicationBuilder, string routeTemplate = "jsonql")
        {
            return applicationBuilder.UseMiddleware<JsonqlMiddleware>(routeTemplate);
        }
    }
}