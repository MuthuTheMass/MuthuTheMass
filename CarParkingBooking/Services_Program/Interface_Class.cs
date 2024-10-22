using ValidateCarParkingDetails.ValidateAuthorization;

namespace CarParkingBooking.Services_Program
{
    public static class Interface_Class
    {
        public static void SeperateServicies(this IServiceCollection services)
        {
            services.AddTransient<IAuthorization, Authorization>();
            services.AddTransient<IDealerData, DealerData>();
            services.AddTransient<IBookingData, BookingData>();
            services.AddTransient<IVehicleData, VehicleData>();
        }
    }
}
