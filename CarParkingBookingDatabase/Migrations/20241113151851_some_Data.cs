using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingBookingDatabase.Migrations
{
    /// <inheritdoc />
    public partial class some_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DealerStoreName",
                table: "DealerDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "UserDetails",
                columns: new[] { "UserID", "Address", "CreatedDate", "Email", "MobileNumber", "Name", "Password", "Rights", "UserProfilePicture" },
                values: new object[] { "User-1", null, new DateTime(2024, 11, 13, 20, 48, 50, 340, DateTimeKind.Unspecified).AddTicks(2328), "balaji@gmail.com", "7896541235", "balaji", "balaji", "User", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserDetails",
                keyColumn: "UserID",
                keyValue: "User-1");

            migrationBuilder.AlterColumn<string>(
                name: "DealerStoreName",
                table: "DealerDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
