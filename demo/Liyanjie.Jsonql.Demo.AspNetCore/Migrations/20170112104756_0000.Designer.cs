using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Liyanjie.Jsonql.Demo.AspNetCore;

namespace Liyanjie.Jsonql.Demo.AspNetCore.Migrations
{
    [DbContext(typeof(DbContext))]
    [Migration("20170112104756_0000")]
    partial class _0000
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("Liyanjie.Jsonql.Demo.AspNetCore.Order", b =>
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

            modelBuilder.Entity("Liyanjie.Jsonql.Demo.AspNetCore.OrderStatusChange", b =>
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

            modelBuilder.Entity("Liyanjie.Jsonql.Demo.AspNetCore.User", b =>
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

            modelBuilder.Entity("Liyanjie.Jsonql.Demo.AspNetCore.UserAccount", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Coins");

                    b.Property<decimal>("Points");

                    b.Property<Guid>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("UserAccounts");
                });

            modelBuilder.Entity("Liyanjie.Jsonql.Demo.AspNetCore.UserAccountRecord", b =>
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

            modelBuilder.Entity("Liyanjie.Jsonql.Demo.AspNetCore.UserProfile", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar")
                        .HasMaxLength(50);

                    b.Property<string>("Nick")
                        .HasMaxLength(50);

                    b.Property<Guid>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Liyanjie.Jsonql.Demo.AspNetCore.Order", b =>
                {
                    b.HasOne("Liyanjie.Jsonql.Demo.AspNetCore.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Liyanjie.Jsonql.Demo.AspNetCore.OrderStatusChange", b =>
                {
                    b.HasOne("Liyanjie.Jsonql.Demo.AspNetCore.Order", "Order")
                        .WithMany("StatusChanges")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Liyanjie.Jsonql.Demo.AspNetCore.UserAccount", b =>
                {
                    b.HasOne("Liyanjie.Jsonql.Demo.AspNetCore.User", "User")
                        .WithOne("Account")
                        .HasForeignKey("Liyanjie.Jsonql.Demo.AspNetCore.UserAccount", "UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Liyanjie.Jsonql.Demo.AspNetCore.UserAccountRecord", b =>
                {
                    b.HasOne("Liyanjie.Jsonql.Demo.AspNetCore.User", "User")
                        .WithMany("AccountRecords")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Liyanjie.Jsonql.Demo.AspNetCore.UserProfile", b =>
                {
                    b.HasOne("Liyanjie.Jsonql.Demo.AspNetCore.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("Liyanjie.Jsonql.Demo.AspNetCore.UserProfile", "UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
