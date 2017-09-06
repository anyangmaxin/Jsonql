using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Liyanjie.Jsonql.Sample.AspNetCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("settings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"settings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddLogging()
                .AddOptions()
                .AddDbContext<DbContext>((provider, builder) =>
                {
                    builder.UseSqlServer(Configuration.GetSection("ConnectionStrings")["SqlServer"]);
                    //builder.UseSqlite(Configuration.GetSection("ConnectionStrings")["Sqlite"]);
                }, ServiceLifetime.Scoped)

                //配置Jsonql的资源列表
                .AddJsonql(options =>
                {
                    var dbContext = services.BuildServiceProvider().GetService<DbContext>();
                    options.Resources
                        .AddResource("Orders", dbContext.Orders)
                        .AddResource("OrderStatusChanges", dbContext.OrderStatusChanges)
                        .AddResource("UserAccounts", dbContext.UserAccounts)
                        .AddResource("UserAccountRecords", dbContext.UserAccountRecords)
                        .AddResource("Users", dbContext.Users)
                        .AddResource("UserProfiles", dbContext.UserProfiles);
                    options.Authorize = context => true;
                    options.JsonqlIncluder = new Liyanjie.Jsonql.DynamicInclude.DynamicJsonqlIncluder();
                    options.JsonqlEvaluator = new Liyanjie.Jsonql.DynamicEvaluation.DynamicJsonqlEvaluator();
                    options.JsonqlLinqer = new Liyanjie.Jsonql.DynamicLinq.DynamicJsonqlLinqer();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app
                .UseJsonql()
                .UseJsonqlTester(options =>
                {

                });

            return;
        }
    }
}
