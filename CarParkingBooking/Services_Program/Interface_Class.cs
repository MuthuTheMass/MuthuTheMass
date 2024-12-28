
using CarParkingSystem.Application.Services.Authorization;
using CarParkingSystem.Application.Services.DealerService;
using CarParkingSystem.Application.Services.UserService;
using CarParkingSystem.Infrastructure.Database.CosmosDatabase.Factory;
using CarParkingSystem.Infrastructure.Repositories;
using CarParkingSystem.Infrastructure.Repositories.CosmosRepository;

namespace CarParkingBooking.Services_Program
{
    public static class Interface_Class
    {
        public static void SeperateServicies(this IServiceCollection services)
        {
            //CosmosClient
            services.AddSingleton<ICosmosClientFactory, CosmosClientFactory>();

            //Services
            services.AddScoped<IAuthorizationService, AuthorizeService>();
            services.AddScoped<IUserProfile, UserProfile>();
            services.AddScoped<IDealerProfile, DealerProfile>();
            
            //Repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDealerRepository, DealerRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();

        }
    }
}
