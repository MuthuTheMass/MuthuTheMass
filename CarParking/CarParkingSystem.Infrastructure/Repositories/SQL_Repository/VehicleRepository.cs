using CarParkingSystem.Domain.Entities.SQL;
using CarParkingSystem.Infrastructure.Database.SQLDatabase.BookingDBContext;
using Microsoft.EntityFrameworkCore;

namespace CarParkingSystem.Infrastructure.Repositories.SQL_Repository;

public interface IVehicleRepository
{
    Task<VehicleDetails?> GetVehicleById(string vehicleId);
    Task<VehicleDetails?> GetVehicleByNumber(string vehicleNumber);
    Task<List<VehicleDetails>>? GetVehicleByName(string name);
    Task<List<VehicleDetails>> GetVehicleByUserId(string userId);
    Task<bool> AddVehicle(VehicleDetails vehicle);
    Task<bool> UpdateVehicle(VehicleDetails vehicle);
    Task<bool> DeleteVehicle(string vehicleId);
}

public class VehicleRepository : IVehicleRepository
{
    private readonly CarParkingBookingDbContext _dbContext;

    public VehicleRepository(CarParkingBookingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<VehicleDetails?> GetVehicleById(string vehicleId)
    {
        var vehicleResource = await _dbContext.VehicleDetails.FindAsync(vehicleId);
        return vehicleResource;
    }

    public async Task<VehicleDetails?> GetVehicleByNumber(string vehicleNumber)
    {
        var vehicleResource = await _dbContext.VehicleDetails.FirstOrDefaultAsync(v => v.VehicleNumber.Contains(vehicleNumber));
        return vehicleResource ?? null;
    }

    public async Task<List<VehicleDetails>>? GetVehicleByName(string name)
    {
        var vehicleResource = await _dbContext.VehicleDetails.Where(v => v.VehicleName == name).ToListAsync();
        return vehicleResource;
    }

    public async Task<bool> AddVehicle(VehicleDetails vehicle)
    {
        var vehicleResource = await _dbContext.VehicleDetails.AnyAsync(v => v.VehicleName == vehicle.VehicleName);

        if (vehicleResource is true) return false;

        await _dbContext.VehicleDetails.AddAsync(vehicle);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateVehicle(VehicleDetails vehicle)
    {
        var vehicleResource = await _dbContext.VehicleDetails.AnyAsync(v => v.VehicleName == vehicle.VehicleName);
        if (vehicleResource is true) return false;
        _dbContext.VehicleDetails.Update(vehicle);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteVehicle(string vehicleId)
    {
        var vehicleResource = await _dbContext.VehicleDetails.Where(v => v.VehicleId == vehicleId).ToListAsync();

        if (vehicleResource.Count == 0) return false;

        _dbContext.VehicleDetails.RemoveRange(vehicleResource);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<VehicleDetails>> GetVehicleByUserId(string userId)
    {
        var vehicleResource = await _dbContext.VehicleDetails.Where(v => v.UserID == userId).ToListAsync();
        return vehicleResource;
    }
}