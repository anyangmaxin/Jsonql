using System.Collections.Generic;
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
                        .AddResource("orders", dbContext.Orders)
                        .AddResource("orderstatuschanges", dbContext.OrderStatusChanges)
                        .AddResource("useraccounts", dbContext.UserAccounts)
                        .AddResource("useraccountrecords", dbContext.UserAccountRecords)
                        .AddResource("users", dbContext.Users)
                        .AddResource("userprofiles", dbContext.UserProfiles);
                    options.Authorize = context => true;
                    options.JsonqlIncluder = new Liyanjie.Jsonql.DynamicInclude.JsonqlIncluder();
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
            //以下初始化数据
            app.UseWhen(httpContext => httpContext.Request.Path.Value == "initial", builder =>
            {
                var db = builder.ApplicationServices.GetService<DbContext>();

                #region 1
                db.Users.Add(new User
                {
                    Username = "1",
                    Profile = new UserProfile
                    {
                        Nick = "Nick1",
                        Avatar = "Avatar1",
                    },
                    Account = new UserAccount
                    {
                        Coins = 1,
                        Points = 1,
                    },
                    Orders = new List<Order>
                     {
                        new Order
                        {
                            Serial = "101",
                            Status = 1,
                            StatusChanges = new List<OrderStatusChange>
                            {
                                new OrderStatusChange { Status = 1 },
                            }
                        },
                        new Order
                        {
                            Serial = "102",
                            Status = 2,
                            StatusChanges = new List<OrderStatusChange>
                            {
                                new OrderStatusChange { Status = 1 },
                                new OrderStatusChange { Status = 2 },
                            }
                        },
                     },
                    AccountRecords = new List<UserAccountRecord>
                     {
                        new UserAccountRecord { Coins = 1, Points = 1, Remark = "101" },
                        new UserAccountRecord { Coins = 1, Points = 1, Remark = "102" },
                     },
                });
                #endregion
                #region 2
                db.Users.Add(new User
                {
                    Username = "2",
                    Profile = new UserProfile
                    {
                        Nick = "Nick2",
                        Avatar = "Avatar2",
                    },
                    Account = new UserAccount
                    {
                        Coins = 1,
                        Points = 1,
                    },
                    Orders = new List<Order>
                     {
                        new Order
                        {
                            Serial = "201",
                            Status = 1,
                            StatusChanges = new List<OrderStatusChange>
                            {
                                new OrderStatusChange { Status = 1 },
                            }
                        },
                        new Order
                        {
                            Serial = "202",
                            Status = 2,
                            StatusChanges = new List<OrderStatusChange>
                            {
                                new OrderStatusChange { Status = 1 },
                                new OrderStatusChange { Status = 2 },
                            }
                        },
                     },
                    AccountRecords = new List<UserAccountRecord>
                     {
                        new UserAccountRecord { Coins = 1, Points = 1, Remark = "201" },
                        new UserAccountRecord { Coins = 1, Points = 1, Remark = "202" },
                     },
                });
                #endregion
                #region 3
                db.Users.Add(new User
                {
                    Username = "3",
                    Profile = new UserProfile
                    {
                        Nick = "Nick3",
                        Avatar = "Avatar3",
                    },
                    Account = new UserAccount
                    {
                        Coins = 1,
                        Points = 1,
                    },
                    Orders = new List<Order>
                     {
                        new Order
                        {
                            Serial = "301",
                            Status = 1,
                            StatusChanges = new List<OrderStatusChange>
                            {
                                new OrderStatusChange { Status = 1 },
                            }
                        },
                        new Order
                        {
                            Serial = "302",
                            Status = 2,
                            StatusChanges = new List<OrderStatusChange>
                            {
                                new OrderStatusChange { Status = 1 },
                                new OrderStatusChange { Status = 2 },
                            }
                        },
                     },
                    AccountRecords = new List<UserAccountRecord>
                     {
                        new UserAccountRecord { Coins = 1, Points = 1, Remark = "301" },
                        new UserAccountRecord { Coins = 1, Points = 1, Remark = "302" },
                     },
                });
                #endregion

                db.SaveChanges();
            });
        }
    }
}
