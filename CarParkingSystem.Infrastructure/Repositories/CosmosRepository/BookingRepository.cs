using CarParkingBooking.QRCodeGenerator.Encription_QRCode_value;
using CarParkingBooking.QRCodeGenerator.Generator;
using CarParkingSystem.Domain.Dtos.Dealers;
using CarParkingSystem.Domain.Helper;
using CarParkingSystem.Infrastructure.Database.CosmosDatabase.Entities;
using CarParkingSystem.Infrastructure.Database.CosmosDatabase.Factory;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace CarParkingSystem.Infrastructure.Repositories.CosmosRepository;

public interface IBookingRepository
{
    Task<CarBooking> GetBooking(string bookingId, string dealerId, string customerId);
    Task<CarBooking> GetByBooking(string bookingId);
    Task<CarBooking?> GetBookingByQR(string EncryptedId);
    Task<List<CarBooking>> GetBookingByDealer(string dealerId);
    Task<bool> AddBookingDetails(CarBooking carBooking);
    Task<bool?> UpdateBookingDetails(CarBooking carBooking);
    Task<bool> DeleteBookingDetails(string bookingId, string dealerId, string customerId);
    Task<List<UserDetailsNewCustomer>> GetUserByConfirmedBookingForDealer(string dealerId);
}

public class BookingRepository : IBookingRepository
{
    private readonly ICosmosClientFactory _cosmosClientFactory;
    private readonly IEncryptionService _encryptService;
    private readonly IQrCodeService _qrCodeService;
    private Container Container;

    public BookingRepository(ICosmosClientFactory cosmosClientFactory, IEncryptionService encryptService,IQrCodeService qrCodeService)
    {
        _cosmosClientFactory = cosmosClientFactory;
        Container = _cosmosClientFactory.GetOrCreateContainerAsync("CarParkingSystem", "BookingData", "/PartitionId").GetAwaiter().GetResult();
        _encryptService = encryptService;
        _qrCodeService = qrCodeService;
    }

    public async Task<bool> AddBookingDetails(CarBooking carBooking)
    {
        try
        {
            var id = await _cosmosClientFactory.GetNextBookingIdAsync("booking_counter");
            carBooking.EncryptedBookingId = await _encryptService.EncryptAsync(id);
            carBooking.GeneratedQrCode = await _qrCodeService.GenerateQrCode(carBooking.EncryptedBookingId);
            PartitionKey partitionKey = new PartitionKey($"{id}_{carBooking.DealerId}_{carBooking.CustomerData.CustomerId}");
            carBooking.PartitionId = $"{id}_{carBooking.DealerId}_{carBooking.CustomerData.CustomerId}";
            carBooking.id = id;
            carBooking.CreatedDate = DateTiming.GetIndianTime();
            var result = await Container.CreateItemAsync(carBooking, partitionKey);
            return result.Resource.DealerId == carBooking.DealerId;
        }
        catch (Exception)
        {
            await _cosmosClientFactory.DecreamentBookingIdAsync("booking_counter");
            return false;
        }
        
    }

    public async Task<bool> DeleteBookingDetails(string bookingId, string dealerId, string customerId)
    {
        string partition = $"{bookingId}-{dealerId}-{customerId}";
        var result = await Container.ReadItemAsync<CarBooking>(bookingId, new PartitionKey(partition));
        if (result.Resource is not null)
        {
            var deleted = await Container.DeleteItemAsync<CarBooking>(bookingId, new PartitionKey(partition));
            return true;
        }
        return false;
    }

    public async Task<CarBooking> GetBooking(string bookingId,string dealerId,string customerId)
    {
        string partition = $"{bookingId}-{dealerId}-{customerId}";
        var result = await Container.ReadItemAsync<CarBooking>(bookingId,new PartitionKey(partition));
        return result.Resource;
    }

    public async Task<List<CarBooking>> GetBookingByDealer(string dealerId)
    {
        var querable = Container.GetItemLinqQueryable<CarBooking>();
        var iterator = querable.Where(d => d.DealerId == dealerId).OrderBy(o => o.BookingDate)
                            .ToFeedIterator();

        var result = new List<CarBooking>();

        while (iterator.HasMoreResults)
        {
            FeedResponse<CarBooking> response = await iterator.ReadNextAsync();
            foreach(var item in response)
            {
                result.Add(item);           
            }
        }
        return result;
    }

    public async Task<CarBooking?> GetBookingByQR(string EncryptedId)
    {
        var querable = Container.GetItemLinqQueryable<CarBooking>();
        var iterator = querable.Where(d => d.EncryptedBookingId == EncryptedId)
                            .ToFeedIterator();
        var result = new List<CarBooking>();

        while (iterator.HasMoreResults)
        {
            FeedResponse<CarBooking> response = await iterator.ReadNextAsync();
            foreach (var item in response)
            {
                result.Add(item);
            }
        }
        return result.SingleOrDefault() ?? null;
    }

    public async Task<CarBooking> GetByBooking(string bookingId)
    {
        var result = await Container.ReadItemAsync<CarBooking>(bookingId, new PartitionKey(string.Empty));
        return result.Resource;
    }

    public async Task<List<UserDetailsNewCustomer?>> GetUserByConfirmedBookingForDealer(string dealerId)
    {
        var queryable = Container.GetItemLinqQueryable<CarBooking>();
        var iterator = queryable.Where(b => b.DealerId != null && b.DealerId.Equals(dealerId))
                                .ToFeedIterator();
        List<UserDetailsNewCustomer?> result = new();

        while (iterator.HasMoreResults)
        {
            FeedResponse<CarBooking> response = await iterator.ReadNextAsync();
            foreach (var item in response)
            {
                result.Add(new UserDetailsNewCustomer(item.CustomerData.CustomerId,item.CreatedDate));
            }
        }
        return result;
    }

    public async Task<bool?> UpdateBookingDetails(CarBooking carBooking)
    {
        var query = Container.GetItemQueryIterator<CarBooking>(
            new QueryDefinition("SELECT * FROM c WHERE c.id = @id")
            .WithParameter("@id", carBooking.id)
        );

        CarBooking? singleBooking = null;

        while (query.HasMoreResults)
        {
            foreach (var item in await query.ReadNextAsync())
            {
                singleBooking = item;
                break;
            }
        }

        if (singleBooking == null)
        {
            Console.WriteLine("User not found.");
            return false;
        }

        singleBooking = carBooking;

        await Container.ReplaceItemAsync(singleBooking, singleBooking.id, new PartitionKey(singleBooking.PartitionId));

        Console.WriteLine($"User {carBooking.id} updated successfully.");
        return true;
    }
}
