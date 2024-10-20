using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingBookingDatabase.Migrations
{
    /// <inheritdoc />
    public partial class profile_pic_remove_foreign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingTripDetails_BookingDetails_BookingID",
                table: "BookingTripDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleDetails_UserDetails_UserID",
                table: "VehicleDetails");

            migrationBuilder.DropIndex(
                name: "IX_VehicleDetails_UserID",
                table: "VehicleDetails");

            migrationBuilder.DropIndex(
                name: "IX_BookingTripDetails_BookingID",
                table: "BookingTripDetails");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "VehicleDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<byte[]>(
                name: "UserProfilePicture",
                table: "UserDetails",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "DealerProfilePicture",
                table: "DealerDetails",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BookingID",
                table: "BookingTripDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserProfilePicture",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "DealerProfilePicture",
                table: "DealerDetails");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "VehicleDetails",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BookingID",
                table: "BookingTripDetails",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleDetails_UserID",
                table: "VehicleDetails",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingTripDetails_BookingID",
                table: "BookingTripDetails",
                column: "BookingID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingTripDetails_BookingDetails_BookingID",
                table: "BookingTripDetails",
                column: "BookingID",
                principalTable: "BookingDetails",
                principalColumn: "BookingID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleDetails_UserDetails_UserID",
                table: "VehicleDetails",
                column: "UserID",
                principalTable: "UserDetails",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
