using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingSystem.Infrastructure.Database.SQLDatabase.Migrations
{
    /// <inheritdoc />
    public partial class address_update_dealer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DealerCity",
                table: "DealerDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DealerCountry",
                table: "DealerDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DealerState",
                table: "DealerDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DealerCity",
                table: "DealerDetails");

            migrationBuilder.DropColumn(
                name: "DealerCountry",
                table: "DealerDetails");

            migrationBuilder.DropColumn(
                name: "DealerState",
                table: "DealerDetails");
        }
    }
}
