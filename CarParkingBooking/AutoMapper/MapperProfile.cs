using AutoMapper;
using CarParkingBookingDatabase.DBModel;
using CarParkingBookingVM.Login;

namespace CarParkingBooking.AutoMapper
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            CreateMap<UserDetails, SignUpVM>()
                .ForMember(opt => opt.UserName, dest => dest.MapFrom(src => src.Name))
                .ForMember(opt => opt.Email, dest => dest.MapFrom(src => src.Email))
                .ForMember(opt => opt.MobileNumber, dest => dest.MapFrom(src => src.MobileNumber))
                .ForMember(opt => opt.Password, dest => dest.MapFrom(src => src.Password)).ReverseMap();
        }
    }
}
