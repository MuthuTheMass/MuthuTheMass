// See https://aka.ms/new-console-template for more information
using DatabaseMigrator.BookingDBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=CarParkingData;Integrated Security=True;Encrypt=False";
var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                // Register DbContext with EnableRetryOnFailure for EF Core 3.x and earlier
                services.AddDbContext<CarParkingBookingDBContext>(options =>
                    options.UseSqlServer(
                        connectionString,
                        sqlOptions => sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5) // Only maxRetryCount is supported in EF Core 3.x and earlier
                    ));
            })
            .Build();

using (var scope = host.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CarParkingBookingDBContext>();
    dbContext.SeedData(); // Seed data when the application starts
}

Console.WriteLine("Data seeded successfully.");