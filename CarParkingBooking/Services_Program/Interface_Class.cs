
using CarParkingSystem.Application.Services.Authorization;
using CarParkingSystem.Infrastructure.Repositories;

namespace CarParkingBooking.Services_Program
{
    public static class Interface_Class
    {
        public static void SeperateServicies(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IAuthorizationService, AuthorizeService>();
            
            //Repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDealerRepository, DealerRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
        }
    }
}
