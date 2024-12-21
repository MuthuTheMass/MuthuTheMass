using CarParkingSystem.Infrastructure.Database.SQLDatabase.BookingDBContext;
using CarParkingSystem.Infrastructure.Database.SQLDatabase.DBModel;
using CarParkingSystem.Infrastructure.DtosHelper;
using Microsoft.EntityFrameworkCore;


namespace CarParkingSystem.Infrastructure.Repositories;


public interface IDealerRepository
{
    Task<DealerDetails?> GetUserByEmail(string email);
    Task<DealerDetails?> GetUserById(string dealerId);
    Task<bool?> GetDealerExists(string dealername);
    Task<List<DealerDetails>> GetAllDealers(Filters filters);
    Task<bool> CreateDealer(DealerDetails dealer);
    Task<bool> UpdateDealer(DealerDetails dealer);
    Task<bool> DeleteDealer(string? emailId);
}

public class DealerRepository : IDealerRepository
{
    private readonly CarParkingBookingDbContext _dbContext;
    public DealerRepository(CarParkingBookingDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<DealerDetails?> GetUserByEmail(string email)
    {
        var dealerResource = await _dbContext.DealerDetails.FirstOrDefaultAsync(d => d.DealerEmail == email);
        return dealerResource;
    }

    public async Task<DealerDetails?> GetUserById(string dealerId)
    {
        var dealerResource = await _dbContext.DealerDetails.FirstOrDefaultAsync(d => d.DealerID == dealerId);
        return dealerResource;
    }

    public async Task<bool?> GetDealerExists(string dealername)
    {
        bool isDealerExists = await _dbContext.DealerDetails.AnyAsync(d => d.DealerName == dealername);
        return isDealerExists;
    }

    public async Task<bool> CreateDealer(DealerDetails dealer)
    {
        bool? dealerResource = await _dbContext.DealerDetails.AnyAsync(d => d.DealerName == dealer.DealerName);
        if (dealerResource is true) return false;
        await _dbContext.DealerDetails.AddAsync(dealer);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateDealer(DealerDetails dealer)
    {
        bool? dealerResource = await _dbContext.DealerDetails.AnyAsync(d => d.DealerName == dealer.DealerName);
        if (dealerResource is null) return false;
        _dbContext.DealerDetails.Update(dealer);
        await _dbContext.SaveChangesAsync();
        return false;
    }

    public async Task<bool> DeleteDealer(string? emailId)
    {
        List<DealerDetails>? dealerResource = await _dbContext.DealerDetails.Where(d => d.DealerEmail == emailId).ToListAsync();
        if (dealerResource.Count <= 0) return false;
        _dbContext.DealerDetails.RemoveRange(dealerResource);
        await _dbContext.SaveChangesAsync();
        return true;


    }

    public async Task<List<DealerDetails>> GetAllDealers(Filters filters)
    {
        //TODO Search area

        List<DealerDetails> queryData = await _dbContext.DealerDetails.ToListAsync();

        return queryData;
    }
}