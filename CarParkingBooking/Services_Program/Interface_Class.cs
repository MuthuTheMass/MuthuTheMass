
using CarParkingSystem.Infrastructure.Repositories;

namespace CarParkingBooking.Services_Program
{
    public static class Interface_Class
    {
        public static void SeperateServicies(this IServiceCollection services)
        {
            // services.AddTransient<IAuthorization, Authorization>();
            // services.AddTransient<IDealerData, DealerData>();
            // services.AddTransient<IBookingData, BookingData>();
            // services.AddTransient<IVehicleData, VehicleData>();
            // services.AddTransient<IUserData, UsersData>();
            
            //Repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDealerRepository, DealerRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
        }
    }
}
