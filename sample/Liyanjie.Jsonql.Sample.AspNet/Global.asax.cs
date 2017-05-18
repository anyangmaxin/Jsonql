using System.Web;

namespace Liyanjie.Jsonql.Sample.AspNet
{
    public class Global : System.Web.HttpApplication
    {
        DbContext dbContext;

        protected void Application_Start()
        {
            dbContext = new DbContext("Data Source=D:\\jsonql.sqlce;Password=123456;Persist Security Info=False");
            //dbContext.Users.Count();
            this.AddJsonql(options =>
            {
                options.Resources
                    .AddResource("orders", dbContext.Orders)
                    .AddResource("orderstatuschanges", dbContext.OrderStatusChanges)
                    .AddResource("useraccounts", dbContext.UserAccounts)
                    .AddResource("useraccountrecords", dbContext.UserAccountRecords)
                    .AddResource("users", dbContext.Users)
                    .AddResource("userprofiles", dbContext.UserProfiles);
                options.Authorize = context => true;
                options.DynamicEvaluator = new Liyanjie.Jsonql.DynamicEvaluation.DynamicEvaluator();
                options.DynamicLinq = new Liyanjie.Jsonql.DynamicLinq.DynamicLinq();
            });
        }

        protected void Application_BeginRequest()
        {
            //var a = Request.Path;
            this.UseJsonql();
            this.UseJsonqlTester();
        }
    }
}
