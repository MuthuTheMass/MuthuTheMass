
using CarParkingBooking.QRCodeGenerator.Encription_QRCode_value;
using CarParkingBooking.QRCodeGenerator.Generator;
using CarParkingSystem.Application.Services.Authorization;
using CarParkingSystem.Application.Services.BookingService;
using CarParkingSystem.Application.Services.DealerService;
using CarParkingSystem.Application.Services.UserService;
using CarParkingSystem.Infrastructure.Database.CosmosDatabase.Factory;
using CarParkingSystem.Infrastructure.Repositories.CosmosRepository;
using CarParkingSystem.Infrastructure.Repositories.SQL_Repository;

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
            services.AddScoped<IUserBookingService, UserBookingService>();

            
            //Repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDealerRepository, DealerRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IDealerSlotsRepository, DealerSlotsRepository>();

            //Encryption
            services.AddScoped<IEncryptionService, EncryptionService>();

            //QRCode
            services.AddScoped<IQrCodeService, QrCodeService>();

        }
    }
}
