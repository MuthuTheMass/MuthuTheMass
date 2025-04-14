using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingSystem.Infrastructure.Database.SQLDatabase.Migrations
{
    /// <inheritdoc />
    public partial class Addvehiclenumberimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "VehicleNumberImage",
                table: "VehicleDetails",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleNumberImage",
                table: "VehicleDetails");
        }
    }
}
