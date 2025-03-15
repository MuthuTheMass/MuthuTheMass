using CarParkingSystem.Domain.Helper;
using CarParkingSystem.Domain.ValueObjects;
using CarParkingSystem.Infrastructure.Database.CosmosDatabase.Entities;
using CarParkingSystem.Infrastructure.Database.CosmosDatabase.Factory;
using CarParkingSystem.Infrastructure.Database.SQLDatabase.BookingDBContext;
using CarParkingSystem.Infrastructure.Repositories.CosmosRepository;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarParkingSystem.Infrastructure;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Configure DbContext options
        var optionsBuilder = new DbContextOptionsBuilder<CarParkingBookingDbContext>();
        optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=CarParkingData;Integrated Security=True;Encrypt=False");

        // Create an instance of DbContext
        using (var dbContext = new CarParkingBookingDbContext(optionsBuilder.Options))
        {
            // Ensure the database is created (optional)
            dbContext.Database.EnsureCreated();

            dbContext.SeedData();
        }

        CosmosClient cosmosClient = new CosmosClient("AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");
        ICosmosClientFactory cosmosClientFactory = new CosmosClientFactory(cosmosClient);
        IBookingRepository bookingRepository = new BookingRepository(cosmosClientFactory);

        var bookingTasks = SeedBookingData().Select(booking => bookingRepository.AddBookingDetails(booking));
        await Task.WhenAll(bookingTasks);

        Console.WriteLine("DB Done");
    }

    public static List<CarBooking> SeedBookingData()
    {
        return new List<CarBooking>
        {
            new CarBooking
            {
                //id = "booking-1",
                PartitionId = "booking-1_Dealer-1_User-1",
                DealerId = "Dealer-1",
                CustomerId = "User-1",
                VehicleInfo = new VehicleDetails
                {
                    VehicleId = "Vehicle-1",
                    VehicleNumber = "TN 01 2345"
                },
                BookingSource = "User",
                BookingDate = new CarBookingDates
                {
                    From = DateTiming.GetIndianTime(),
                    To = null
                },
                CreatedDate = DateTiming.GetIndianTime().AddHours(-5),
                UpdatedDate = null,
                IsDeleted = false,
                GeneratedQrCode = "QR-1",
                AdvanceAmount = "1000",
                BookingStatus = new Status
                {
                    State = BookingProcessDetails.InProgress,
                    Reason = "Booking InProgress"
                },
                AllottedSlots = "A1"
            },
            new CarBooking
            {
                //id = "booking-2",
                PartitionId = "booking-2_Dealer-2_User-2",
                DealerId = "Dealer-2",
                CustomerId = "User-2",
                VehicleInfo = new VehicleDetails
                {
                    VehicleId = "Vehicle-2",
                    VehicleNumber = "TN 02 3456"
                },
                BookingSource = "Offline",
                BookingDate = new CarBookingDates
                {
                    From = DateTiming.GetIndianTime(),
                    To = null
                },
                CreatedDate = DateTiming.GetIndianTime().AddDays(5),
                UpdatedDate = null,
                IsDeleted = false,
                GeneratedQrCode = "QR-2",
                AdvanceAmount = "2000",
                BookingStatus = new Status
                {
                    State = BookingProcessDetails.SlotConfirmed,
                    Reason = "Booking Confirmed"
                },
                AllottedSlots = "A2"
            }
        };
    }
}