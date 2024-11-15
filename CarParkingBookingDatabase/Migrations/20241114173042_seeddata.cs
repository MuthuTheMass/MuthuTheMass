using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingBookingDatabase.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserDetails",
                keyColumn: "UserID",
                keyValue: "User-1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserDetails",
                columns: new[] { "UserID", "Address", "CreatedDate", "Email", "MobileNumber", "Name", "Password", "Rights", "UserProfilePicture" },
                values: new object[] { "User-1", null, new DateTime(2024, 11, 13, 20, 48, 50, 340, DateTimeKind.Unspecified).AddTicks(2328), "balaji@gmail.com", "7896541235", "balaji", "balaji", "User", null });
        }
    }
}
