﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DataCenterContext))]
    partial class DataCenterContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AdditionalPowerSubscription", b =>
                {
                    b.Property<int>("AdditionalPowersId")
                        .HasColumnType("int");

                    b.Property<int>("SubscriptionsId")
                        .HasColumnType("int");

                    b.HasKey("AdditionalPowersId", "SubscriptionsId");

                    b.HasIndex("SubscriptionsId");

                    b.ToTable("AdditionalPowerSubscription");
                });

            modelBuilder.Entity("Infrastructure.Models.AdditionalPower", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Power")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<decimal>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("AdditionalPowers");
                });

            modelBuilder.Entity("Infrastructure.Models.Companion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("nvarchar(max)")
                        .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");

                    b.Property<string>("IdentityNo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<short>("IdentityType")
                        .HasColumnType("smallint");

                    b.Property<string>("JobTitle")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("VisitId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("VisitId");

                    b.ToTable("Companions");
                });

            modelBuilder.Entity("Infrastructure.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("nvarchar(320)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PrimaryPhone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("SecondaryPhone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<short>("Status")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Infrastructure.Models.CustomerFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<short>("DocType")
                        .HasColumnType("smallint");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerFiles");
                });

            modelBuilder.Entity("Infrastructure.Models.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<string>("InvoiceNo")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime");

                    b.Property<short>("Status")
                        .HasColumnType("smallint");

                    b.Property<int>("SubscriptionId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Infrastructure.Models.Representive", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("nvarchar(320)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("nvarchar(max)")
                        .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");

                    b.Property<string>("IdentityNo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<short>("IdentityType")
                        .HasColumnType("smallint");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<short>("Status")
                        .HasColumnType("smallint");

                    b.Property<int?>("VisitId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("CustomerId");

                    b.HasIndex("VisitId");

                    b.ToTable("Representives");
                });

            modelBuilder.Entity("Infrastructure.Models.RepresentiveFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<short>("DocType")
                        .HasColumnType("smallint");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RepresentiveId")
                        .HasColumnType("int");

                    b.Property<int>("RepresintiveId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("RepresentiveId");

                    b.ToTable("RepresentiveFiles");
                });

            modelBuilder.Entity("Infrastructure.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AcpPort")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AmountOfPower")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Dns")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("MonthlyVisits")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)");

                    b.Property<short>("Status")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("Infrastructure.Models.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime");

                    b.Property<short>("Status")
                        .HasColumnType("smallint");

                    b.Property<Guid?>("SubscriptionFileId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("TotalPrice")
                        .IsRequired()
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("Infrastructure.Models.SubscriptionFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubscriptionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("SubscriptionId")
                        .IsUnique();

                    b.ToTable("SubscriptionFiles");
                });

            modelBuilder.Entity("Infrastructure.Models.TransactionHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<short>("Action")
                        .HasColumnType("smallint");

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("EntityData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("EntityType")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("TransactionHistories");
                });

            modelBuilder.Entity("Infrastructure.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<int>("EmpId")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<short>("Status")
                        .HasColumnType("smallint");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Infrastructure.Models.Visit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("ExpectedEndTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("ExpectedStartTime")
                        .HasColumnType("datetime");

                    b.Property<int?>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<decimal>("Price")
                        .HasPrecision(6, 2)
                        .HasColumnType("decimal(6,2)");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime");

                    b.Property<int>("SubscriptionId")
                        .HasColumnType("int");

                    b.Property<int>("TimeShiftId")
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("TotalMin")
                        .HasColumnType("time");

                    b.Property<short>("VisitTypeId")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("SubscriptionId");

                    b.HasIndex("TimeShiftId");

                    b.HasIndex("VisitTypeId");

                    b.ToTable("Visits");
                });

            modelBuilder.Entity("Infrastructure.Models.VisitTimeShift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("PriceForFirstHour")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.Property<decimal>("PriceForRemainingHour")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<short>("Status")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("VisitTimeShifts");
                });

            modelBuilder.Entity("Infrastructure.Models.VisitType", b =>
                {
                    b.Property<short>("Id")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("VisitTypes");
                });

            modelBuilder.Entity("AdditionalPowerSubscription", b =>
                {
                    b.HasOne("Infrastructure.Models.AdditionalPower", null)
                        .WithMany()
                        .HasForeignKey("AdditionalPowersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Models.Subscription", null)
                        .WithMany()
                        .HasForeignKey("SubscriptionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Infrastructure.Models.AdditionalPower", b =>
                {
                    b.HasOne("Infrastructure.Models.User", "CreatedBy")
                        .WithMany("AdditionalPowersCreatedBy")
                        .HasForeignKey("CreatedById")
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("Infrastructure.Models.Companion", b =>
                {
                    b.HasOne("Infrastructure.Models.User", "CreatedBy")
                        .WithMany("CompanionsCreatedBy")
                        .HasForeignKey("CreatedById")
                        .IsRequired();

                    b.HasOne("Infrastructure.Models.Visit", "Visit")
                        .WithMany("Companions")
                        .HasForeignKey("VisitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("Visit");
                });

            modelBuilder.Entity("Infrastructure.Models.Customer", b =>
                {
                    b.HasOne("Infrastructure.Models.User", "CreatedBy")
                        .WithMany("CustomersCreatedBy")
                        .HasForeignKey("CreatedById")
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("Infrastructure.Models.CustomerFile", b =>
                {
                    b.HasOne("Infrastructure.Models.User", "CreatedBy")
                        .WithMany("CustomerFilesCreatedBy")
                        .HasForeignKey("CreatedById")
                        .IsRequired();

                    b.HasOne("Infrastructure.Models.Customer", "Customer")
                        .WithMany("Files")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Infrastructure.Models.Invoice", b =>
                {
                    b.HasOne("Infrastructure.Models.User", "CreatedBy")
                        .WithMany("InvoicesCreatedBy")
                        .HasForeignKey("CreatedById")
                        .IsRequired();

                    b.HasOne("Infrastructure.Models.Subscription", "Subscription")
                        .WithMany("Invoices")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("Infrastructure.Models.Representive", b =>
                {
                    b.HasOne("Infrastructure.Models.User", "CreatedBy")
                        .WithMany("RepresentivesCreatedBy")
                        .HasForeignKey("CreatedById")
                        .IsRequired();

                    b.HasOne("Infrastructure.Models.Customer", "Customer")
                        .WithMany("Representives")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Models.Visit", null)
                        .WithMany("Representives")
                        .HasForeignKey("VisitId");

                    b.Navigation("CreatedBy");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Infrastructure.Models.RepresentiveFile", b =>
                {
                    b.HasOne("Infrastructure.Models.User", "CreatedBy")
                        .WithMany("RepresentiveFilesCreatedBy")
                        .HasForeignKey("CreatedById")
                        .IsRequired();

                    b.HasOne("Infrastructure.Models.Representive", "Representive")
                        .WithMany("Files")
                        .HasForeignKey("RepresentiveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("Representive");
                });

            modelBuilder.Entity("Infrastructure.Models.Service", b =>
                {
                    b.HasOne("Infrastructure.Models.User", "CreatedBy")
                        .WithMany("ServicesCreatedBy")
                        .HasForeignKey("CreatedById")
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("Infrastructure.Models.Subscription", b =>
                {
                    b.HasOne("Infrastructure.Models.User", "CreatedBy")
                        .WithMany("SubscriptionsCreatedBy")
                        .HasForeignKey("CreatedById")
                        .IsRequired();

                    b.HasOne("Infrastructure.Models.Customer", "Customer")
                        .WithMany("Subscriptions")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Models.Service", "Service")
                        .WithMany("Subscriptions")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("Customer");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("Infrastructure.Models.SubscriptionFile", b =>
                {
                    b.HasOne("Infrastructure.Models.User", "CreatedBy")
                        .WithMany("SubscriptionFilesCreatedBy")
                        .HasForeignKey("CreatedById")
                        .IsRequired();

                    b.HasOne("Infrastructure.Models.Subscription", "Subscription")
                        .WithOne("SubscriptionFile")
                        .HasForeignKey("Infrastructure.Models.SubscriptionFile", "SubscriptionId")
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("Infrastructure.Models.TransactionHistory", b =>
                {
                    b.HasOne("Infrastructure.Models.User", "CreatedBy")
                        .WithMany("TransactionHistorysCreatedBy")
                        .HasForeignKey("CreatedById")
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("Infrastructure.Models.User", b =>
                {
                    b.HasOne("Infrastructure.Models.User", "CreatedBy")
                        .WithMany("UsersCreatedBy")
                        .HasForeignKey("CreatedById");

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("Infrastructure.Models.Visit", b =>
                {
                    b.HasOne("Infrastructure.Models.User", "CreatedBy")
                        .WithMany("VisitsCreatedBy")
                        .HasForeignKey("CreatedById")
                        .IsRequired();

                    b.HasOne("Infrastructure.Models.Invoice", "Invoice")
                        .WithMany("Visits")
                        .HasForeignKey("InvoiceId");

                    b.HasOne("Infrastructure.Models.Subscription", null)
                        .WithMany("Visits")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Models.VisitTimeShift", "TimeShift")
                        .WithMany()
                        .HasForeignKey("TimeShiftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Models.VisitType", "VisitType")
                        .WithMany()
                        .HasForeignKey("VisitTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("Invoice");

                    b.Navigation("TimeShift");

                    b.Navigation("VisitType");
                });

            modelBuilder.Entity("Infrastructure.Models.VisitTimeShift", b =>
                {
                    b.HasOne("Infrastructure.Models.User", "CreatedBy")
                        .WithMany("VisitTimeShiftsCreatedBy")
                        .HasForeignKey("CreatedById")
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("Infrastructure.Models.Customer", b =>
                {
                    b.Navigation("Files");

                    b.Navigation("Representives");

                    b.Navigation("Subscriptions");
                });

            modelBuilder.Entity("Infrastructure.Models.Invoice", b =>
                {
                    b.Navigation("Visits");
                });

            modelBuilder.Entity("Infrastructure.Models.Representive", b =>
                {
                    b.Navigation("Files");
                });

            modelBuilder.Entity("Infrastructure.Models.Service", b =>
                {
                    b.Navigation("Subscriptions");
                });

            modelBuilder.Entity("Infrastructure.Models.Subscription", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("SubscriptionFile")
                        .IsRequired();

                    b.Navigation("Visits");
                });

            modelBuilder.Entity("Infrastructure.Models.User", b =>
                {
                    b.Navigation("AdditionalPowersCreatedBy");

                    b.Navigation("CompanionsCreatedBy");

                    b.Navigation("CustomerFilesCreatedBy");

                    b.Navigation("CustomersCreatedBy");

                    b.Navigation("InvoicesCreatedBy");

                    b.Navigation("RepresentiveFilesCreatedBy");

                    b.Navigation("RepresentivesCreatedBy");

                    b.Navigation("ServicesCreatedBy");

                    b.Navigation("SubscriptionFilesCreatedBy");

                    b.Navigation("SubscriptionsCreatedBy");

                    b.Navigation("TransactionHistorysCreatedBy");

                    b.Navigation("UsersCreatedBy");

                    b.Navigation("VisitTimeShiftsCreatedBy");

                    b.Navigation("VisitsCreatedBy");
                });

            modelBuilder.Entity("Infrastructure.Models.Visit", b =>
                {
                    b.Navigation("Companions");

                    b.Navigation("Representives");
                });
#pragma warning restore 612, 618
        }
    }
}
