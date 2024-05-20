﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Subscription_based_marketing.DataContext;

#nullable disable

namespace Subscription_based_marketing.Migrations
{
    [DbContext(typeof(SubscriptionDbContext))]
    partial class SubscriptionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Subscription_based_marketing.Models.Adminstrator.AdminstratorAccount", b =>
                {
                    b.Property<Guid>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AdminAccountCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AdminEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("AdminLastLoginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AdminPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminUserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminId");

                    b.ToTable("Adminstrators");
                });

            modelBuilder.Entity("Subscription_based_marketing.Models.Seller.SellerAccount", b =>
                {
                    b.Property<Guid>("SellerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SellerAccountCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SellerDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SellerLastLoginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SellerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellerPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("SellerPhoneNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("SellerUserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SellerID");

                    b.ToTable("SellerAccounts");
                });

            modelBuilder.Entity("Subscription_based_marketing.Models.ServiceControl.ServiceAccessControl", b =>
                {
                    b.Property<Guid>("ServiceAccessControlID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AccessEndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("AccessStartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("AccessStatus")
                        .HasColumnType("int");

                    b.Property<Guid>("SellerID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("serviceID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ServiceAccessControlID");

                    b.HasIndex("SellerID");

                    b.HasIndex("UserId");

                    b.HasIndex("serviceID");

                    b.ToTable("ServiceAccessControl");
                });

            modelBuilder.Entity("Subscription_based_marketing.Models.Services.ServiceDetail", b =>
                {
                    b.Property<Guid>("ServiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SellerID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ServiceCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ServiceDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ServiceDuration")
                        .HasColumnType("int");

                    b.Property<string>("ServiceFeature")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ServiceIsPublish")
                        .HasColumnType("bit");

                    b.Property<int>("ServiceLevel")
                        .HasColumnType("int");

                    b.Property<decimal>("ServicePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ServiceTermsCondition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServiceID");

                    b.HasIndex("SellerID");

                    b.ToTable("ServiceDetails");
                });

            modelBuilder.Entity("Subscription_based_marketing.Models.Subscription.SubscriptionDetails", b =>
                {
                    b.Property<Guid>("SubscriptionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SubscriptionBillingFrequency")
                        .HasColumnType("int");

                    b.Property<DateTime>("SubscriptionNextBillingDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SubscriptionStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SubscriptionStatus")
                        .HasColumnType("int");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("serviceID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SubscriptionID");

                    b.HasIndex("UserID");

                    b.HasIndex("serviceID");

                    b.ToTable("SubscriptionDetails");
                });

            modelBuilder.Entity("Subscription_based_marketing.Models.User.UserAccount", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AccountCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SubscriptionStatus")
                        .HasColumnType("bit");

                    b.Property<string>("UserEmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UserLastLoginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Subscription_based_marketing.Models.ServiceControl.ServiceAccessControl", b =>
                {
                    b.HasOne("Subscription_based_marketing.Models.Seller.SellerAccount", "seller")
                        .WithMany()
                        .HasForeignKey("SellerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Subscription_based_marketing.Models.User.UserAccount", "user")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Subscription_based_marketing.Models.Services.ServiceDetail", "Service")
                        .WithMany()
                        .HasForeignKey("serviceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");

                    b.Navigation("seller");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Subscription_based_marketing.Models.Services.ServiceDetail", b =>
                {
                    b.HasOne("Subscription_based_marketing.Models.Seller.SellerAccount", "seller")
                        .WithMany()
                        .HasForeignKey("SellerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("seller");
                });

            modelBuilder.Entity("Subscription_based_marketing.Models.Subscription.SubscriptionDetails", b =>
                {
                    b.HasOne("Subscription_based_marketing.Models.User.UserAccount", "user")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Subscription_based_marketing.Models.Services.ServiceDetail", "Service")
                        .WithMany()
                        .HasForeignKey("serviceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");

                    b.Navigation("user");
                });
#pragma warning restore 612, 618
        }
    }
}
