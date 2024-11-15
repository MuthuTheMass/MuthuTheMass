using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingBookingDatabase.Migrations
{
    /// <inheritdoc />
    public partial class tripdetialsadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingDetials",
                table: "BookingDetials");

            migrationBuilder.RenameTable(
                name: "BookingDetials",
                newName: "BookingDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingDetails",
                table: "BookingDetails",
                column: "BookingID");

            migrationBuilder.CreateTable(
                name: "BookingTripDetails",
                columns: table => new
                {
                    TripId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TripName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TripDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingTripDetails", x => x.TripId);
                    table.ForeignKey(
                        name: "FK_BookingTripDetails_BookingDetails_BookingID",
                        column: x => x.BookingID,
                        principalTable: "BookingDetails",
                        principalColumn: "BookingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingTripDetails_BookingID",
                table: "BookingTripDetails",
                column: "BookingID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingTripDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingDetails",
                table: "BookingDetails");

            migrationBuilder.RenameTable(
                name: "BookingDetails",
                newName: "BookingDetials");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingDetials",
                table: "BookingDetials",
                column: "BookingID");
        }
    }
}
