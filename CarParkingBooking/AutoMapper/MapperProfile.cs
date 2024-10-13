using AutoMapper;
using CarParkingBookingDatabase.DBModel;
using CarParkingBookingVM.Authorization;
using CarParkingBookingVM.Login;
using CarParkingBookingVM.VM_S.Booking;
using CarParkingBookingVM.VM_S.Dealers;
using CarParkingBookingVM.VM_S.Vehicle;
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

            CreateMap<DealerSignUpVM, DealerDetails>() 
                .ForMember(opt => opt.DealerName, dest => dest.MapFrom(src => src.Name))
                .ForMember(opt => opt.DealerEmail, dest => dest.MapFrom(src => src.Email))
                .ForMember(opt => opt.DealerPhoneNo, dest => dest.MapFrom(src => src.PhoneNo))
                .ForMember(opt => opt.DealerPassword, dest => dest.MapFrom(src => src.Password))
                .ReverseMap();

            CreateMap<DealerDetails, AuthorizedDealerLoginVM>()
                .ForMember(opt => opt.Email, dest => dest.MapFrom(src => src.DealerEmail))
                .ForMember(opt => opt.UserName, dest => dest.MapFrom(src => src.DealerName))
                .ReverseMap()
                ;

            CreateMap<VehicleDetails, Vehicle_User_VM>()
                .ForMember(opt => opt.VehicleId, dest => dest.MapFrom(src => src.VehicleId))
                .ForMember(opt => opt.VehicleNumber, dest => dest.MapFrom(src => src.VehicleNumber))
                .ForMember(opt => opt.VehicleImage, dest => dest.MapFrom(src => ConvertByteToFromFile(src.VehicleImage)))
                .ForMember(opt => opt.VehicleModel, dest => dest.MapFrom(src => src.VehicleModel))
                .ForMember(opt => opt.Alternative_Phone_Number, dest => dest.MapFrom(src => src.Alternative_Phone_Number))
                ;

            CreateMap<Vehicle_User_VM,VehicleDetails>()
                .ForMember(opt => opt.VehicleId, dest => dest.MapFrom(src => src.VehicleId))
                .ForMember(opt => opt.VehicleNumber, dest => dest.MapFrom(src => src.VehicleNumber))
                .ForMember(opt => opt.VehicleImage, dest => dest.MapFrom(src => ConvertFileToByte(src.VehicleImage)))
                .ForMember(opt => opt.VehicleModel, dest => dest.MapFrom(src => src.VehicleModel))
                .ForMember(opt => opt.Alternative_Phone_Number, dest => dest.MapFrom(src => src.Alternative_Phone_Number))
                ;

            CreateMap<VehicleVM, VehicleDetails>()
                .ForMember(opt => opt.VehicleNumber,dest => dest.MapFrom(src => src.VehicleNumber))
                .ForMember(opt => opt.VehicleName,dest => dest.MapFrom(src => src.VehicleName))
                .ForMember(opt => opt.Alternative_Phone_Number,dest => dest.MapFrom(src => src.Alternative_Phone_Number))
                .ForMember(opt => opt.DriverName,dest => dest.MapFrom(src => src.DriverName))
                .ForMember(opt => opt.DriverPhoneNumber,dest => dest.MapFrom(src => src.DriverPhoneNumber))
                .ForMember(opt => opt.VehicleImage,dest => dest.MapFrom(src => ConvertFileToByte(src.VehicleImage)))
                .ForMember(opt => opt.VehicleModel,dest => dest.MapFrom(src => src.VehicleModel))
                ;

            CreateMap<VehicleDetails, VehicleVM>()
                .ForMember(opt => opt.VehicleNumber, dest => dest.MapFrom(src => src.VehicleNumber))
                .ForMember(opt => opt.VehicleName, dest => dest.MapFrom(src => src.VehicleName))
                .ForMember(opt => opt.Alternative_Phone_Number, dest => dest.MapFrom(src => src.Alternative_Phone_Number))
                .ForMember(opt => opt.DriverName, dest => dest.MapFrom(src => src.DriverName))
                .ForMember(opt => opt.DriverPhoneNumber, dest => dest.MapFrom(src => src.DriverPhoneNumber))
                .ForMember(opt => opt.VehicleImage, dest => dest.MapFrom(src => ConvertByteToFromFile(src.VehicleImage)))
                .ForMember(opt => opt.VehicleModel, dest => dest.MapFrom(src => src.VehicleModel))
                ;
        }
    }
}
