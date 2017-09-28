using Liyanjie.Jsonql.Core;

namespace Liyanjie.Jsonql.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new Liyanjie.Jsonql.Sample.AspNetCore.DbContext())
            {
                var resources = new ResourceTable()
                    .AddResource("Orders", dbContext.Orders)
                    .AddResource("OrderStatusChanges", dbContext.OrderStatusChanges)
                    .AddResource("UserAccounts", dbContext.UserAccounts)
                    .AddResource("UserAccountRecords", dbContext.UserAccountRecords)
                    .AddResource("Users", dbContext.Users)
                    .AddResource("UserProfiles", dbContext.UserProfiles);
                var jsonqlIncluder = new Liyanjie.Jsonql.DynamicInclude.DynamicJsonqlIncluder();
                var jsonqlEvaluator = new Liyanjie.Jsonql.DynamicEvaluation.DynamicJsonqlEvaluator();
                var jsonqlLinqer = new Liyanjie.Jsonql.DynamicLinq.DynamicJsonqlLinqer();

                var query = @"
{
    min: UserAccounts[].select(Coins).min()
}
";
                using (var queryHandler = new QueryHandler(resources, jsonqlIncluder, jsonqlLinqer, jsonqlEvaluator))
                {
                    var handler = queryHandler.Handle(query, null);
                    handler.Wait();
                    var content = handler.Result;
                    System.Console.WriteLine(content);
                }
            }
        }
    }
}
