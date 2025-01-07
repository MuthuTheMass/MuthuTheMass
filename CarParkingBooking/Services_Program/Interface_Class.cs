
using CarParkingSystem.Application.Services.Authorization;
using CarParkingSystem.Application.Services.DealerService;
using CarParkingSystem.Application.Services.UserService;
using CarParkingSystem.Infrastructure.Repositories;

namespace CarParkingBooking.Services_Program
{
    public static class Interface_Class
    {
        public static void SeperateServicies(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IAuthorizationService, AuthorizeService>();
            services.AddScoped<IUserProfile, UserProfile>();
            services.AddScoped<IDealerProfile, DealerProfile>();
            
            //Repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDealerRepository, DealerRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
        }
    }
}
