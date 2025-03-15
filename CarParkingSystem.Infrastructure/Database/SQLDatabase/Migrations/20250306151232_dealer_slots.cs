using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingSystem.Infrastructure.Database.SQLDatabase.Migrations
{
    /// <inheritdoc />
    public partial class dealer_slots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DealerSlotDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DealerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Available_Slots = table.Column<int>(type: "int", nullable: false),
                    Booked_Slots = table.Column<int>(type: "int", nullable: false),
                    Total_Slots = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealerSlotDetails", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DealerSlotDetails");
        }
    }
}
