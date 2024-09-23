using AutoMapper;
using CarParkingBookingDatabase.DBModel;
using CarParkingBookingVM.Login;
using CarParkingBookingVM.VM_S.Dealers;
using System.Text.Json;

namespace CarParkingBooking.AutoMapper
{
    public class MapperProfile : MapperHelper
    {

        public MapperProfile()
        {
            CreateMap<UserDetails, SignUpVM>()
                .ForMember(opt => opt.UserName, dest => dest.MapFrom(src => src.Name))
                .ForMember(opt => opt.Email, dest => dest.MapFrom(src => src.Email))
                .ForMember(opt => opt.MobileNumber, dest => dest.MapFrom(src => src.MobileNumber))
                .ForMember(opt => opt.Password, dest => dest.MapFrom(src => src.Password))
                .ReverseMap();

            CreateMap<DealerVM,DealerDetails>()
                .ForMember(opt => opt.DealerGPSLocation, dest => dest.MapFrom(src => convert(src.DealerGPSLocation)))
                .ReverseMap();
        }
    }
}
