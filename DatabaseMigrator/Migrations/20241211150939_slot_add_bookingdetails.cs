using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseMigrator.Migrations
{
    /// <inheritdoc />
    public partial class slot_add_bookingdetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slot_Confirmation",
                table: "BookingDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slot_Id",
                table: "BookingDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slot_Name",
                table: "BookingDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slot_Confirmation",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "Slot_Id",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "Slot_Name",
                table: "BookingDetails");
        }
    }
}
