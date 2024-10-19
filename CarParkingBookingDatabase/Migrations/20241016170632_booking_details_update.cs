using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingBookingDatabase.Migrations
{
    /// <inheritdoc />
    public partial class booking_details_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dealer_Name",
                table: "BookingDetials");

            migrationBuilder.DropColumn(
                name: "Dealer_PhoneNumber",
                table: "BookingDetials");

            migrationBuilder.DropColumn(
                name: "Phone_Number",
                table: "BookingDetials");

            migrationBuilder.DropColumn(
                name: "RC_Book_File",
                table: "BookingDetials");

            migrationBuilder.DropColumn(
                name: "RC_Book_Number",
                table: "BookingDetials");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "BookingDetials");

            migrationBuilder.RenameColumn(
                name: "Vehicle_Size_Type",
                table: "BookingDetials",
                newName: "Vehicle_Id");

            migrationBuilder.RenameColumn(
                name: "Vehicle_Number",
                table: "BookingDetials",
                newName: "User_ID");

            migrationBuilder.RenameColumn(
                name: "Vehicle_Image",
                table: "BookingDetials",
                newName: "Dealer_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Vehicle_Id",
                table: "BookingDetials",
                newName: "Vehicle_Size_Type");

            migrationBuilder.RenameColumn(
                name: "User_ID",
                table: "BookingDetials",
                newName: "Vehicle_Number");

            migrationBuilder.RenameColumn(
                name: "Dealer_ID",
                table: "BookingDetials",
                newName: "Vehicle_Image");

            migrationBuilder.AddColumn<string>(
                name: "Dealer_Name",
                table: "BookingDetials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Dealer_PhoneNumber",
                table: "BookingDetials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone_Number",
                table: "BookingDetials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RC_Book_File",
                table: "BookingDetials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RC_Book_Number",
                table: "BookingDetials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "BookingDetials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
