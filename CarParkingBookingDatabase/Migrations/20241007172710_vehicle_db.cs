using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingBookingDatabase.Migrations
{
    /// <inheritdoc />
    public partial class vehicle_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DealerPassword",
                table: "dealerDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "vehicleDetails",
                columns: table => new
                {
                    VehicleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VehicleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverPhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    VehicleModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alternative_Phone_Number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicleDetails", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_vehicleDetails_userDetails_UserID",
                        column: x => x.UserID,
                        principalTable: "userDetails",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_vehicleDetails_UserID",
                table: "vehicleDetails",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vehicleDetails");

            migrationBuilder.DropColumn(
                name: "DealerPassword",
                table: "dealerDetails");
        }
    }
}
