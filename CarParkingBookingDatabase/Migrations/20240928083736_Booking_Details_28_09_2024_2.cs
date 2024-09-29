using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingBookingDatabase.Migrations
{
    /// <inheritdoc />
    public partial class Booking_Details_28_09_2024_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Vehicle_Image",
                table: "bookingDetials",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vehicle_Image",
                table: "bookingDetials");
        }
    }
}
