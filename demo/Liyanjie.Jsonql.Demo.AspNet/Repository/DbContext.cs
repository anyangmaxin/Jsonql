using System.Collections.Generic;
using System.Data.Entity;

namespace Liyanjie.Jsonql.Demo.AspNet
{
    public class DbContext : System.Data.Entity.DbContext
    {
        public DbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Database.SetInitializer(new DbInitialier());
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderStatusChange> OrderStatusChanges { get; set; }

        public DbSet<UserAccount> UserAccounts { get; set; }

        public DbSet<UserAccountRecord> UserAccountRecords { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    public class DbInitialier : CreateDatabaseIfNotExists<DbContext>
    {
        protected override void Seed(DbContext context)
        {
            base.Seed(context);

            #region 1
            context.Users.Add(new User
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
            context.Users.Add(new User
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
            context.Users.Add(new User
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

            context.SaveChanges();
        }
    }
}
