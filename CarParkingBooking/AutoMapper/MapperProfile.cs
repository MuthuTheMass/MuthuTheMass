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
                .ForMember(opt => opt.UserName,     dest => dest.MapFrom(src => src.Name))
                .ForMember(opt => opt.Email,        dest => dest.MapFrom(src => src.Email))
                .ForMember(opt => opt.MobileNumber, dest => dest.MapFrom(src => src.MobileNumber))
                .ForMember(opt => opt.Password,     dest => dest.MapFrom(src => src.Password))
                .ReverseMap();

            CreateMap<DealerVM,DealerDetails>()
                .ForMember(opt => opt.DealerName,        dest => dest.MapFrom(src => src.DealerName))
                .ForMember(opt => opt.DealerEmail,       dest => dest.MapFrom(src => src.DealerEmail))
                .ForMember(opt => opt.DealerPhoneNo,     dest => dest.MapFrom(src => src.DealerPhoneNo))
                .ForMember(opt => opt.DealerDescription, dest => dest.MapFrom(src => src.DealerDescription))
                .ForMember(opt => opt.DealerStartDate,   dest => dest.MapFrom(src => src.DealerStartDate))
                .ForMember(opt => opt.DealerTiming,      dest => dest.MapFrom(src => ConvertTimingString(src.DealerTiming)))
                .ForMember(opt => opt.DealerAddress,     dest => dest.MapFrom(src => src.DealerAddress))
                .ForMember(opt => opt.DealerLandmark,    dest => dest.MapFrom(src => src.DealerLandmark))
                .ForMember(opt => opt.DealerGPSLocation, dest => dest.MapFrom(src => ConvertString(src.DealerGPSLocation)))
                .ForMember(opt => opt.DealerRating,      dest => dest.MapFrom(src => src.DealerRating));

            CreateMap<DealerDetails, DealerVM>()
                .ForMember(opt => opt.DealerGPSLocation, dest => dest.MapFrom(src => ConvertGPS(src.DealerGPSLocation)))
                .ForMember(opt => opt.DealerTiming,      dest => dest.MapFrom(src => ConvertStringTiming(src.DealerTiming)));
        }
    }
}
