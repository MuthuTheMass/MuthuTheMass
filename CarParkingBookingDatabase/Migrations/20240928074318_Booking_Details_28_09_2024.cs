using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingBookingDatabase.Migrations
{
    /// <inheritdoc />
    public partial class Booking_Details_28_09_2024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Dealer_",
                table: "bookingDetials",
                newName: "Dealer_PhoneNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Dealer_PhoneNumber",
                table: "bookingDetials",
                newName: "Dealer_");
        }
    }
}
