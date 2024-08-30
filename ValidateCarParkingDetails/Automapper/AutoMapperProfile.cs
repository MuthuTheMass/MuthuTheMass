using AutoMapper;
using CarParkingBookingDatabase.DBModel;
using CarParkingBookingVM.Login;

namespace CarParkingBooking.Automapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SignUpVM,UserDetails>()
                .ForMember(dest => dest.Name,opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.MobileNumber, opt => opt.MapFrom(src => src.MobileNumber))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        }
    }
}
