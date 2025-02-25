using Azure;
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
    Task<bool> AddBookingDetails(CarBooking carBooking);
    Task<CarBooking> UpdateBookingDetails(CarBooking carBooking);
    Task<bool> DeleteBookingDetails(string bookingId, string dealerId, string customerId);
    Task<List<string>> GetUserByBookingForDealer(string dealerId);
}

public class BookingRepository : IBookingRepository
{
    private readonly ICosmosClientFactory _cosmosClientFactory;
    private Container Container;

    public BookingRepository(ICosmosClientFactory cosmosClientFactory)
    {
        _cosmosClientFactory = cosmosClientFactory;
        Container = _cosmosClientFactory.GetOrCreateContainerAsync("CarParkingSystem", "BookingData", "/PartitionId").GetAwaiter().GetResult();
    }

    public async Task<bool> AddBookingDetails(CarBooking carBooking)
    {
        try
        {
            var id = await _cosmosClientFactory.GetNextBookingIdAsync("booking_counter");
            PartitionKey partitionKey = new PartitionKey($"{id}_{carBooking.DealerId}_{carBooking.CustomerId}");
            carBooking.PartitionId = $"{id}_{carBooking.DealerId}_{carBooking.CustomerId}";
            carBooking.id = id;
            carBooking.CreatedDate = DateTiming.GetIndianTime().ToString();
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

    public async Task<CarBooking> GetByBooking(string bookingId)
    {
        var result = await Container.ReadItemAsync<CarBooking>(bookingId, new PartitionKey(string.Empty));
        return result.Resource;
    }

    public async Task<List<string>> GetUserByBookingForDealer(string dealerId)
    {
        var queryable = Container.GetItemLinqQueryable<CarBooking>();
        var iterator = queryable.Where(b => b.DealerId.Equals(dealerId)).Select(u=> u.CustomerId).Distinct().ToFeedIterator();
        List<string> result = new();

        while (iterator.HasMoreResults)
        {
            FeedResponse<string> response = await iterator.ReadNextAsync();
            foreach (var item in response)
            {
                result.Add(item);
            }
        }
        return result;
    }

    public Task<CarBooking> UpdateBookingDetails(CarBooking carBooking)
    {
        throw new NotImplementedException();
    }
}