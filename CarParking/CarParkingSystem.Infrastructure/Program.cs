using CarParkingBooking.QRCodeGenerator.Encription_QRCode_value;
using CarParkingBooking.QRCodeGenerator.Generator;
using CarParkingSystem.Domain.Helper;
using CarParkingSystem.Domain.ValueObjects;
using CarParkingSystem.Infrastructure.Database.CosmosDatabase.Entities;
using CarParkingSystem.Infrastructure.Database.CosmosDatabase.Factory;
using CarParkingSystem.Infrastructure.Database.SQLDatabase.BookingDBContext;
using CarParkingSystem.Infrastructure.Repositories.CosmosRepository;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;

namespace CarParkingSystem.Infrastructure;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Configure DbContext options
        var optionsBuilder = new DbContextOptionsBuilder<CarParkingBookingDbContext>();
        optionsBuilder.UseSqlServer(
            "Data Source=.\\SQLEXPRESS;Initial Catalog=CarParkingData;Integrated Security=True;Encrypt=False");

        // Create an instance of DbContext
        using (var dbContext = new CarParkingBookingDbContext(optionsBuilder.Options))
        {
            // Ensure the database is created (optional)
            dbContext.Database.EnsureCreated();

            dbContext.SeedData();
        }

        CosmosClient cosmosClient =
            new CosmosClient(
                "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");
        ICosmosClientFactory cosmosClientFactory = new CosmosClientFactory(cosmosClient);
        IEncryptionService _encryptService = new EncryptionService();
        IQrCodeService _qrCodeService = new QrCodeService();
        IBookingRepository bookingRepository =
            new BookingRepository(cosmosClientFactory, _encryptService, _qrCodeService);

        foreach (var booking in SeedBookingData())
        {
            await bookingRepository.AddBookingDetails(booking);
        }


        Console.WriteLine("DB Done");
    }

    public static List<CarBooking> SeedBookingData()
    {
        return new List<CarBooking>
        {
            new CarBooking
            {
                //id = "booking-1",
                //PartitionId = "booking-1_Dealer-1_User-1",
                DealerId = "Dealer-1",
                CustomerData = new CustomerUserDetails()
                {
                    CustomerId = "User-1",
                    CustomerAddress = "Chennai",
                    CustomerEmail = "balaji@gmail.com",
                    CustomerMobileNumber = "9876543210",
                    CustomerName = "Balaji",
                    CustomerAuthorityOfIssue = "RTO",
                    CustomerProof = "Aadhar",
                    CustomerProofNumber = "1234567890"
                },
                VehicleInfo = new VehicleInformation
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
                GeneratedQrCode = new byte[0],
                Payment = new PaymentInfo()
                {
                    AdvanceAmount = "50",
                    CreatedDate = DateTiming.GetIndianTime(),
                    Due_Amount = null,
                    Final_Amount = (decimal.Parse("50") - null).ToString(),
                    Source = BookingSources.User,
                    status = BookingStatus.Pending,
                    TransactionId = Guid.NewGuid().ToString(),
                    UpdatedDate = null,
                },
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
                //PartitionId = "booking-2_Dealer-2_User-2",
                DealerId = "Dealer-2",
                CustomerData = new CustomerUserDetails()
                {
                    CustomerId = "User-2",
                    CustomerAddress = "Chennai",
                    CustomerEmail = "muthu@gmail.com",
                    CustomerMobileNumber = "9876543210",
                    CustomerName = "Muthu",
                    CustomerAuthorityOfIssue = "RTO",
                    CustomerProof = "Aadhar",
                    CustomerProofNumber = "1234567890"
                },
                VehicleInfo = new VehicleInformation
                {
                    VehicleId = "Vehicle-2",
                    VehicleNumber = "TN 02 3456"
                },
                BookingSource = "Offline",
                BookingDate = new CarBookingDates
                {
                    UserBookingDate = DateTiming.GetIndianTime(),
                    From = null,
                    To = null
                },
                CreatedDate = DateTiming.GetIndianTime().AddDays(5),
                UpdatedDate = null,
                IsDeleted = false,
                GeneratedQrCode = new byte[1],
                Payment = new PaymentInfo()
                {
                    AdvanceAmount = "50",
                    CreatedDate = DateTiming.GetIndianTime(),
                    Due_Amount = null,
                    Final_Amount = (decimal.Parse("50") - null).ToString(),
                    Source = BookingSources.User,
                    status = BookingStatus.Pending,
                    TransactionId = Guid.NewGuid().ToString(),
                    UpdatedDate = null,
                },
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