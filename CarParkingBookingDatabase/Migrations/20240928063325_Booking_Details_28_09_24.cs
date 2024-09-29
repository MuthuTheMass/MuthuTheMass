using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingBookingDatabase.Migrations
{
    /// <inheritdoc />
    public partial class Booking_Details_28_09_24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "userDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Owner_Name",
                table: "userDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Owner_PhoneNo",
                table: "userDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RC_Book_Image",
                table: "userDetails",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RC_Book_Number",
                table: "userDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DealerStartDate",
                table: "dealerDetails",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rights",
                table: "dealerDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "bookingDetials",
                columns: table => new
                {
                    BookingID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone_Number = table.Column<string>(type: "nvarchar(max)", nullable: false), 
                    Vehicle_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vehicle_Size_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dealer_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dealer_ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArrivingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExitTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookingDetials", x => x.BookingID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bookingDetials");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "userDetails");

            migrationBuilder.DropColumn(
                name: "Owner_Name",
                table: "userDetails");

            migrationBuilder.DropColumn(
                name: "Owner_PhoneNo",
                table: "userDetails");

            migrationBuilder.DropColumn(
                name: "RC_Book_Image",
                table: "userDetails");

            migrationBuilder.DropColumn(
                name: "RC_Book_Number",
                table: "userDetails");

            migrationBuilder.DropColumn(
                name: "Rights",
                table: "dealerDetails");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DealerStartDate",
                table: "dealerDetails",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
