using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Liyanjie.Jsonql.Sample.AspNetCore;

namespace Liyanjie.Jsonql.Sample.AspNetCore.Migrations
{
    [DbContext(typeof(DbContext))]
    partial class DbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Liyanjie.Jsonql.Sample.AspNetCore.Order", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreateTime");

                    b.Property<string>("Serial");

                    b.Property<int>("Status");

                    b.Property<Guid>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Liyanjie.Jsonql.Sample.AspNetCore.OrderStatusChange", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreateTime");

                    b.Property<Guid>("OrderID");

                    b.Property<int>("Status");

                    b.HasKey("ID");

                    b.HasIndex("OrderID");

                    b.ToTable("OrderStatusChanges");
                });

            modelBuilder.Entity("Liyanjie.Jsonql.Sample.AspNetCore.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreateTime");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Liyanjie.Jsonql.Sample.AspNetCore.UserAccount", b =>
                {
                    b.Property<Guid>("ID");

                    b.Property<decimal>("Coins");

                    b.Property<decimal>("Points");

                    b.HasKey("ID");

                    b.ToTable("UserAccounts");
                });

            modelBuilder.Entity("Liyanjie.Jsonql.Sample.AspNetCore.UserAccountRecord", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Coins");

                    b.Property<DateTimeOffset>("CreateTime");

                    b.Property<decimal>("Points");

                    b.Property<string>("Remark")
                        .HasMaxLength(50);

                    b.Property<Guid>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("UserAccountRecords");
                });

            modelBuilder.Entity("Liyanjie.Jsonql.Sample.AspNetCore.UserProfile", b =>
                {
                    b.Property<Guid>("ID");

                    b.Property<string>("Avatar")
                        .HasMaxLength(50);

                    b.Property<string>("Nick")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Liyanjie.Jsonql.Sample.AspNetCore.Order", b =>
                {
                    b.HasOne("Liyanjie.Jsonql.Sample.AspNetCore.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Liyanjie.Jsonql.Sample.AspNetCore.OrderStatusChange", b =>
                {
                    b.HasOne("Liyanjie.Jsonql.Sample.AspNetCore.Order", "Order")
                        .WithMany("StatusChanges")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Liyanjie.Jsonql.Sample.AspNetCore.UserAccount", b =>
                {
                    b.HasOne("Liyanjie.Jsonql.Sample.AspNetCore.User", "User")
                        .WithOne("Account")
                        .HasForeignKey("Liyanjie.Jsonql.Sample.AspNetCore.UserAccount", "ID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Liyanjie.Jsonql.Sample.AspNetCore.UserAccountRecord", b =>
                {
                    b.HasOne("Liyanjie.Jsonql.Sample.AspNetCore.User", "User")
                        .WithMany("AccountRecords")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Liyanjie.Jsonql.Sample.AspNetCore.UserProfile", b =>
                {
                    b.HasOne("Liyanjie.Jsonql.Sample.AspNetCore.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("Liyanjie.Jsonql.Sample.AspNetCore.UserProfile", "ID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
