using AutoMapper;
using CarParkingBookingDatabase.DBModel;
using CarParkingBookingVM.Login;
using CarParkingBookingVM.VM_S.Booking;
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

            CreateMap<BookingDetails, BookingVM>()
                .ForMember(opt => opt.UserName, dest => dest.MapFrom(src => src.UserName))
                .ForMember(opt => opt.Phone_Number, dest => dest.MapFrom(src => src.Phone_Number))
                .ForMember(opt => opt.Vehicle_Number, dest => dest.MapFrom(src => src.Vehicle_Number))
                .ForMember(opt => opt.Vehicle_Size_Type, dest => dest.MapFrom(src => src.Vehicle_Size_Type))
                .ForMember(opt => opt.RC_Book_Number , dest => dest.MapFrom(src => src.RC_Book_Number))
                .ForMember(opt => opt.RC_Book_File, dest => dest.MapFrom(src => convertByteToFromFile(src.RC_Book_File)))
                .ForMember(opt => opt.Vehicle_Image, dest => dest.MapFrom(src => convertByteToFromFile(src.Vehicle_Image)))
                .ForMember(opt => opt.Dealer_Name, dest => dest.MapFrom(src => src.Dealer_Name))
                .ForMember(opt => opt.Dealer_PhoneNumber, dest => dest.MapFrom(src => src.Dealer_PhoneNumber))
                .ForMember(opt => opt.Driver_Name, dest => dest.MapFrom(src => src.Driver_Name))
                .ForMember(opt => opt.Driver_PhoneNumber, dest => dest.MapFrom(src => src.Driver_PhoneNumber))
                .ForMember(opt => opt.ArrivingTime, dest => dest.MapFrom(src => src.ArrivingTime ))
                ;

            CreateMap<BookingVM, BookingDetails>()
                .ForMember(opt => opt.RC_Book_File, dest => dest.MapFrom(src => convertFileToByte(src.RC_Book_File)))
                .ForMember(opt => opt.Vehicle_Image, dest => dest.MapFrom(src => convertFileToByte(src.Vehicle_Image)))
                ;
                
        }
    }
}
