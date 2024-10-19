using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingBookingDatabase.Migrations
{
    /// <inheritdoc />
    public partial class user_details_remove_vehicle_no : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vehicleDetails_userDetails_UserID",
                table: "vehicleDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_vehicleDetails",
                table: "vehicleDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userDetails",
                table: "userDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dealerDetails",
                table: "dealerDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bookingDetials",
                table: "bookingDetials");

            migrationBuilder.DropColumn(
                name: "VehicleNumber",
                table: "userDetails");

            migrationBuilder.DropColumn(
                name: "DealerStartDate",
                table: "dealerDetails");

            migrationBuilder.RenameTable(
                name: "vehicleDetails",
                newName: "VehicleDetails");

            migrationBuilder.RenameTable(
                name: "userDetails",
                newName: "UserDetails");

            migrationBuilder.RenameTable(
                name: "dealerDetails",
                newName: "DealerDetails");

            migrationBuilder.RenameTable(
                name: "bookingDetials",
                newName: "BookingDetials");

            migrationBuilder.RenameIndex(
                name: "IX_vehicleDetails_UserID",
                table: "VehicleDetails",
                newName: "IX_VehicleDetails_UserID");

            migrationBuilder.AddColumn<bool>(
                name: "DealerOpenOrClosed",
                table: "DealerDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleDetails",
                table: "VehicleDetails",
                column: "VehicleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDetails",
                table: "UserDetails",
                column: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DealerDetails",
                table: "DealerDetails",
                column: "DealerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingDetials",
                table: "BookingDetials",
                column: "BookingID");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleDetails_UserDetails_UserID",
                table: "VehicleDetails",
                column: "UserID",
                principalTable: "UserDetails",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleDetails_UserDetails_UserID",
                table: "VehicleDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleDetails",
                table: "VehicleDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDetails",
                table: "UserDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DealerDetails",
                table: "DealerDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingDetials",
                table: "BookingDetials");

            migrationBuilder.DropColumn(
                name: "DealerOpenOrClosed",
                table: "DealerDetails");

            migrationBuilder.RenameTable(
                name: "VehicleDetails",
                newName: "vehicleDetails");

            migrationBuilder.RenameTable(
                name: "UserDetails",
                newName: "userDetails");

            migrationBuilder.RenameTable(
                name: "DealerDetails",
                newName: "dealerDetails");

            migrationBuilder.RenameTable(
                name: "BookingDetials",
                newName: "bookingDetials");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleDetails_UserID",
                table: "vehicleDetails",
                newName: "IX_vehicleDetails_UserID");

            migrationBuilder.AddColumn<string>(
                name: "VehicleNumber",
                table: "userDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DealerStartDate",
                table: "dealerDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_vehicleDetails",
                table: "vehicleDetails",
                column: "VehicleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userDetails",
                table: "userDetails",
                column: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dealerDetails",
                table: "dealerDetails",
                column: "DealerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bookingDetials",
                table: "bookingDetials",
                column: "BookingID");

            migrationBuilder.AddForeignKey(
                name: "FK_vehicleDetails_userDetails_UserID",
                table: "vehicleDetails",
                column: "UserID",
                principalTable: "userDetails",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
