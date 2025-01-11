using CarParkingSystem.Domain.Entities.SQL;
using CarParkingSystem.Infrastructure.Database.SQLDatabase.BookingDBContext;
using CarParkingSystem.Infrastructure.Repositories.CosmosRepository;
using Microsoft.EntityFrameworkCore;

namespace CarParkingSystem.Infrastructure.Repositories;

public interface IUserRepository
{
    Task<UserDetails?> GetUserByEmail(string email);
    Task<UserDetails?> GetUserById(string id);
    Task<List<UserDetails>> GetUsers();
    Task<bool> UpdateUserByEmailId(UserDetails user);
    Task<bool> CraeteNewUser(UserDetails user);
    Task<bool> DeleteUserByEmailId(string email);
    Task<List<UserDetails>> GetUserDetailsForDealer(string dealerId);
}

public class UserRepository : IUserRepository
{
    private readonly CarParkingBookingDbContext _dbContext;
    private readonly IBookingRepository _bookingRepository;

    public UserRepository(CarParkingBookingDbContext dbContext,IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
        _dbContext = dbContext; 
    }
    
    public async Task<UserDetails?> GetUserByEmail(string email)
    {
        var userResource = await _dbContext.UserDetails.FirstOrDefaultAsync(u => u.Email == email);
        return userResource;
    }

    public async Task<UserDetails?> GetUserById(string id)
    {
        var userResource = await _dbContext.UserDetails.FirstOrDefaultAsync(u => u.UserID == id);
        return userResource;
    }

    public async Task<List<UserDetails>> GetUsers()
    {
        var userResource = await _dbContext.UserDetails.ToListAsync();
        return userResource;
    }

    public async Task<bool> UpdateUserByEmailId(UserDetails user)
    {
        var userResource = await _dbContext.UserDetails.FirstOrDefaultAsync(u => u.Email == user.Email);
        if (userResource == null)
        {
            return false;
        }
        _dbContext.Update(user);
        await _dbContext.SaveChangesAsync();
        return true;

        
    }

    public async Task<bool> CraeteNewUser(UserDetails user)
    {
        var userResource = await _dbContext.UserDetails.AnyAsync(u => u.Email == user.Email);
        if (!userResource)
        {
            _dbContext.UserDetails.Add(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteUserByEmailId(string email)
    {
        var userResource = await _dbContext.UserDetails.Where(u=>u.Email == email).ToListAsync();

        if (userResource.Count == 0)
        {
            return false;
        }

        userResource.ForEach(u => _dbContext.UserDetails.Remove(u));
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<UserDetails>> GetUserDetailsForDealer(string dealerId)
    {
        List<UserDetails> userDetails = new List<UserDetails>();
        var usersId = await _bookingRepository.GetUserByBookingForDealer(dealerId);
        var userResource = await _dbContext.UserDetails.Where(u => usersId.Contains(u.UserID)).ToListAsync();
        return userResource;
        
    }
}