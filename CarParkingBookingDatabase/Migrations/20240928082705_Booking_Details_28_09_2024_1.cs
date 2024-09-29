using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingBookingDatabase.Migrations
{
    /// <inheritdoc />
    public partial class Booking_Details_28_09_2024_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExitTime",
                table: "bookingDetials");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "bookingDetials",
                newName: "RC_Book_Number");

            migrationBuilder.AddColumn<string>(
                name: "Driver_Name",
                table: "bookingDetials",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Driver_PhoneNumber",
                table: "bookingDetials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "RC_Book_File",
                table: "bookingDetials",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Driver_Name",
                table: "bookingDetials");

            migrationBuilder.DropColumn(
                name: "Driver_PhoneNumber",
                table: "bookingDetials");

            migrationBuilder.DropColumn(
                name: "RC_Book_File",
                table: "bookingDetials");

            migrationBuilder.RenameColumn(
                name: "RC_Book_Number",
                table: "bookingDetials",
                newName: "Address");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExitTime",
                table: "bookingDetials",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
