﻿// <auto-generated />
using System;
using CarParkingBookingDatabase.BookingDBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarParkingBookingDatabase.Migrations
{
    [DbContext(typeof(CarParkingBookingDBContext))]
    [Migration("20241008175216_vehicle_identity")]
    partial class vehicle_identity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CarParkingBookingDatabase.DBModel.BookingDetails", b =>
                {
                    b.Property<string>("BookingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ArrivingTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Dealer_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dealer_PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Driver_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Driver_PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone_Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RC_Book_File")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RC_Book_Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vehicle_Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vehicle_Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vehicle_Size_Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookingID");

                    b.ToTable("bookingDetials");
                });

            modelBuilder.Entity("CarParkingBookingDatabase.DBModel.DealerDetails", b =>
                {
                    b.Property<string>("DealerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DealerAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DealerDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DealerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DealerGPSLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DealerLandmark")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DealerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DealerPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DealerPhoneNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DealerRating")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DealerStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DealerTiming")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rights")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DealerID");

                    b.ToTable("dealerDetails");
                });

            modelBuilder.Entity("CarParkingBookingDatabase.DBModel.UserDetails", b =>
                {
                    b.Property<string>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rights")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehicleNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("userDetails");
                });

            modelBuilder.Entity("CarParkingBookingDatabase.DBModel.VehicleDetails", b =>
                {
                    b.Property<string>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Alternative_Phone_Number")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("DriverName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DriverPhoneNumber")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("VehicleImage")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("VehicleModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehicleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehicleNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VehicleId");

                    b.HasIndex("UserID");

                    b.ToTable("vehicleDetails");
                });

            modelBuilder.Entity("CarParkingBookingDatabase.DBModel.VehicleDetails", b =>
                {
                    b.HasOne("CarParkingBookingDatabase.DBModel.UserDetails", "UserDetails")
                        .WithMany("VehicleDetails")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserDetails");
                });

            modelBuilder.Entity("CarParkingBookingDatabase.DBModel.UserDetails", b =>
                {
                    b.Navigation("VehicleDetails");
                });
#pragma warning restore 612, 618
        }
    }
}