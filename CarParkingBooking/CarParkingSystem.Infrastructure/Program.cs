using CarParkingSystem.Infrastructure.Database.SQLDatabase.BookingDBContext;
using Microsoft.EntityFrameworkCore;

namespace CarParkingSystem.Infrastructure;

public class Program
{
    public static void Main(string[] args)
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
    }
}