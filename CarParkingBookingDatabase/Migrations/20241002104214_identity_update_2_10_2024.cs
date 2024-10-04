using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingBookingDatabase.Migrations
{
    /// <inheritdoc />
    public partial class identity_update_2_10_2024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner_Name",
                table: "userDetails");

            migrationBuilder.DropColumn(
                name: "Owner_PhoneNo",
                table: "userDetails");

            migrationBuilder.DropColumn(
                name: "RC_Book_Image",
                table: "userDetails");

            migrationBuilder.RenameColumn(
                name: "RC_Book_Number",
                table: "userDetails",
                newName: "VehicleNumber");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "userDetails",
                newName: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VehicleNumber",
                table: "userDetails",
                newName: "RC_Book_Number");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "userDetails",
                newName: "ID");

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
        }
    }
}
