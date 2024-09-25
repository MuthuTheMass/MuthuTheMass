using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingBookingDatabase.Migrations
{
    /// <inheritdoc />
    public partial class dealerdetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dealerDetails",
                columns: table => new
                {
                    DealerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "100000, 1"),
                    DealerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerPhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerStartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DealerTiming = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerLandmark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerGPSLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerRating = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dealerDetails", x => x.DealerID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dealerDetails");
        }
    }
}
