using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkingSystem.Infrastructure.Database.SQLDatabase.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DealerDetails",
                columns: table => new
                {
                    DealerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DealerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerProfilePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DealerStoreName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerPhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealerTiming = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealerAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealerCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealerState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealerCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealerLandmark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealerGPSLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealerRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rights = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DealerOpenOrClosed = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsValidUser = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealerDetails", x => x.DealerID);
                });

            migrationBuilder.CreateTable(
                name: "UserDetails",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserProfilePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rights = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetails", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "VehicleDetails",
                columns: table => new
                {
                    VehicleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VehicleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverPhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    VehicleModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alternative_Phone_Number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleDetails", x => x.VehicleId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DealerDetails");

            migrationBuilder.DropTable(
                name: "UserDetails");

            migrationBuilder.DropTable(
                name: "VehicleDetails");
        }
    }
}
