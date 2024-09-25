using AutoMapper;

namespace CarParkingBooking.Automapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
                // Add more profiles if needed
            });

            return config.CreateMapper();
        }
    }
}
