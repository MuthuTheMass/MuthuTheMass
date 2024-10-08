using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingBookingDatabase.Migrations
{
    /// <inheritdoc />
    public partial class initial_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bookingDetials",
                columns: table => new
                {
                    BookingID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vehicle_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vehicle_Size_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RC_Book_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RC_Book_File = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vehicle_Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dealer_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dealer_PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Driver_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Driver_PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArrivingTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookingDetials", x => x.BookingID);
                });

            migrationBuilder.CreateTable(
                name: "dealerDetails",
                columns: table => new
                {
                    DealerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DealerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerPhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DealerTiming = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerLandmark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerGPSLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerRating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rights = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dealerDetails", x => x.DealerID);
                });

            migrationBuilder.CreateTable(
                name: "userDetails",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rights = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userDetails", x => x.UserID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bookingDetials");

            migrationBuilder.DropTable(
                name: "dealerDetails");

            migrationBuilder.DropTable(
                name: "userDetails");
        }
    }
}
