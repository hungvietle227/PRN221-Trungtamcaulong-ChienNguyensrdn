﻿// <auto-generated />
using System;
using BadmintonCenter.BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BadmintonCenter.BusinessObject.Migrations
{
    [DbContext(typeof(BadmintonDbContext))]
    partial class BadmintonDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("BookingTypeId")
                        .HasColumnType("int");

                    b.Property<string>("DateOfWeek")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpiredDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<double>("TotalHour")
                        .HasColumnType("float");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValidDate")
                        .HasColumnType("datetime2");

                    b.HasKey("BookingId");

                    b.HasIndex("BookingTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.BookingDetail", b =>
                {
                    b.Property<int>("CourtId")
                        .HasColumnType("int");

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<int>("TimeSlotId")
                        .HasColumnType("int");

                    b.HasKey("CourtId", "BookingId", "TimeSlotId");

                    b.HasIndex("BookingId");

                    b.HasIndex("TimeSlotId");

                    b.ToTable("BookingDetails");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.BookingType", b =>
                {
                    b.Property<int>("BookingTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingTypeId"));

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("BookingTypeId");

                    b.ToTable("BookingTypes");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.Court", b =>
                {
                    b.Property<int>("CourtId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourtId"));

                    b.Property<string>("CourtName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("CourtId");

                    b.ToTable("Courts");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.Package", b =>
                {
                    b.Property<int>("PackageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PackageId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("HourIncluded")
                        .HasColumnType("int");

                    b.Property<string>("PackageName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("PackageId");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.PaymentMethod", b =>
                {
                    b.Property<int>("PaymentMethodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentMethodId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("MethodName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("PaymentMethodId");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<int>("RoleName")
                        .HasColumnType("int");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.TimeSlot", b =>
                {
                    b.Property<int>("SlotTimeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SlotTimeId"));

                    b.Property<string>("EndTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("StartTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Time")
                        .HasColumnType("float");

                    b.HasKey("SlotTimeId");

                    b.ToTable("TimeSlots");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<int?>("BookingId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PackageId")
                        .HasColumnType("int");

                    b.Property<int>("PaymentMethodId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TransactionId");

                    b.HasIndex("PackageId");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.UserPackage", b =>
                {
                    b.Property<int>("PackageId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("HourRemaining")
                        .HasColumnType("int");

                    b.Property<int>("ValidInMonth")
                        .HasColumnType("int");

                    b.HasKey("PackageId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPackages");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.Booking", b =>
                {
                    b.HasOne("BadmintonCenter.BusinessObject.Models.Transaction", "Transaction")
                        .WithOne("Booking")
                        .HasForeignKey("BadmintonCenter.BusinessObject.Models.Booking", "BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BadmintonCenter.BusinessObject.Models.BookingType", "BookingType")
                        .WithMany("Bookings")
                        .HasForeignKey("BookingTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BadmintonCenter.BusinessObject.Models.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("BookingType");

                    b.Navigation("Transaction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.BookingDetail", b =>
                {
                    b.HasOne("BadmintonCenter.BusinessObject.Models.Booking", "Booking")
                        .WithMany("BookingDetails")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BadmintonCenter.BusinessObject.Models.Court", "Court")
                        .WithMany("BookingDetails")
                        .HasForeignKey("CourtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BadmintonCenter.BusinessObject.Models.TimeSlot", "TimeSlot")
                        .WithMany("BookingDetails")
                        .HasForeignKey("TimeSlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("Court");

                    b.Navigation("TimeSlot");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.Transaction", b =>
                {
                    b.HasOne("BadmintonCenter.BusinessObject.Models.Package", "Package")
                        .WithMany("Transactions")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BadmintonCenter.BusinessObject.Models.PaymentMethod", "PaymentMethod")
                        .WithMany("Transactions")
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BadmintonCenter.BusinessObject.Models.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Package");

                    b.Navigation("PaymentMethod");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.User", b =>
                {
                    b.HasOne("BadmintonCenter.BusinessObject.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.UserPackage", b =>
                {
                    b.HasOne("BadmintonCenter.BusinessObject.Models.Package", "Package")
                        .WithMany("UserPackages")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BadmintonCenter.BusinessObject.Models.User", "User")
                        .WithMany("UserPackages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Package");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.Booking", b =>
                {
                    b.Navigation("BookingDetails");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.BookingType", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.Court", b =>
                {
                    b.Navigation("BookingDetails");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.Package", b =>
                {
                    b.Navigation("Transactions");

                    b.Navigation("UserPackages");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.PaymentMethod", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.TimeSlot", b =>
                {
                    b.Navigation("BookingDetails");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.Transaction", b =>
                {
                    b.Navigation("Booking");
                });

            modelBuilder.Entity("BadmintonCenter.BusinessObject.Models.User", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Transactions");

                    b.Navigation("UserPackages");
                });
#pragma warning restore 612, 618
        }
    }
}
