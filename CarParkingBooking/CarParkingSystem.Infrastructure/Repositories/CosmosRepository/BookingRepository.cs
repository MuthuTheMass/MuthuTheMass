using CarParkingSystem.Infrastructure.Database.CosmosDatabase.Entities;
using CarParkingSystem.Infrastructure.Database.CosmosDatabase.Factory;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CarParkingSystem.Infrastructure.Repositories.CosmosRepository;

public interface IBookingRepository
{
    Task<CarBooking> GetBooking(string bookingId, string dealerId, string customerId);
    Task<CarBooking> GetByBooking(string bookingId);
    Task<CarBooking> AddBookingDetails(CarBooking carBooking);
    Task<CarBooking> UpdateBookingDetails(CarBooking carBooking);
    Task<bool> DeleteBookingDetails(string bookingId, string dealerId, string customerId);
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

    public async Task<CarBooking> AddBookingDetails(CarBooking carBooking)
    {
        var result = await Container.CreateItemAsync(carBooking);
        return result;
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

    public Task<CarBooking> UpdateBookingDetails(CarBooking carBooking)
    {
        throw new NotImplementedException();
    }
}