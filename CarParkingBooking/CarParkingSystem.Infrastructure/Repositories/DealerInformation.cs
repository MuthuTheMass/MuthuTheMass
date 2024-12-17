using CarParkingSystem.Infrastructure.Database.SQLDatabase.DBModel;

namespace CarParkingSystem.Infrastructure.Repositories;


public interface IDealerInformation
{
    Task<UserDetails?> GetUserByEmail(string email);
    Task<UserDetails?> GetUserById(int userId);
    Task<bool?> GetDealerExists(string username);
    Task<bool> CreateDealer(DealerDetails? user);
    Task<bool> UpdateDealer(DealerDetails? user);
    Task<bool> DeleteDealer(string? emailId);
}

public class DealerInformation
{
    
}