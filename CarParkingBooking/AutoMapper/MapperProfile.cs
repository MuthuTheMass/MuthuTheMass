﻿using AutoMapper;
using CarParkingBookingVM.Authorization;
using CarParkingBookingVM.Enums;
using CarParkingBookingVM.Login;
using CarParkingBookingVM.VM_S;
using CarParkingBookingVM.VM_S.Booking;
using CarParkingBookingVM.VM_S.Dealers;
using CarParkingBookingVM.VM_S.Vehicle;
using DatabaseMigrator.DBModel;
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

            CreateMap<DealerVM, DealerDetails>()
                .ForMember(opt => opt.DealerName, dest => dest.MapFrom(src => src.DealerName))
                .ForMember(opt => opt.DealerEmail, dest => dest.MapFrom(src => src.DealerEmail))
                .ForMember(opt => opt.DealerPhoneNo, dest => dest.MapFrom(src => src.DealerPhoneNo))
                .ForMember(opt => opt.DealerDescription, dest => dest.MapFrom(src => src.DealerDescription))
                .ForMember(opt => opt.DealerTiming, dest => dest.MapFrom(src => ConvertTimingString(src.DealerTiming)))
                .ForMember(opt => opt.DealerAddress, dest => dest.MapFrom(src => src.DealerAddress))
                .ForMember(opt => opt.DealerLandmark, dest => dest.MapFrom(src => src.DealerLandmark))
                .ForMember(opt => opt.DealerGPSLocation, dest => dest.MapFrom(src => src.DealerLocationURL))
                .ForMember(opt => opt.DealerRating, dest => dest.MapFrom(src => src.DealerRating))
                .ForMember(opt => opt.DealerStoreName, dest => dest.MapFrom(src => src.DealerStoreName))
                ;

            CreateMap<DealerDetails, DealerVM>()
                .ForMember(opt => opt.DealerTiming, dest => dest.MapFrom(src => ConvertStringTiming(src.DealerTiming)))
                .ForMember(opt => opt.DealerLocationURL, dest => dest.MapFrom(src => src.DealerGPSLocation));
;

            CreateMap<BookingDetails, BookingVM>()
                .ForMember(opt => opt.User_ID, dest => dest.MapFrom(src => src.User_ID))
                .ForMember(opt => opt.Vehicle_Id, dest => dest.MapFrom(src => src.Vehicle_Id))
                .ForMember(opt => opt.Driver_Name, dest => dest.MapFrom(src => src.Driver_Name))
                .ForMember(opt => opt.Driver_PhoneNumber, dest => dest.MapFrom(src => src.Driver_PhoneNumber))
                .ForMember(opt => opt.ArrivingTime, dest => dest.MapFrom(src => src.ArrivingTime))
                .ReverseMap()
                ;

            //CreateMap<BookingVM, BookingDetails>()
            //    .ForMember(opt => opt.RC_Book_File, dest => dest.MapFrom(src => convertFileToByte(src.RC_Book_File)))
            //    .ForMember(opt => opt.Vehicle_Image, dest => dest.MapFrom(src => convertFileToByte(src.Vehicle_Image)))
            //    ;

            CreateMap<DealerSignUpVM, DealerDetails>()
                .ForMember(dest => dest.DealerName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DealerEmail, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.DealerPhoneNo, opt => opt.MapFrom(src => src.PhoneNo))
                .ForMember(dest => dest.DealerPassword, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Rights, opt => opt.MapFrom(src => AccessToUsers.Dealer.ToString())) // Default value
                .ForMember(dest => dest.DealerDescription, opt => opt.Ignore())
                .ForMember(dest => dest.DealerTiming, opt => opt.Ignore())
                .ForMember(dest => dest.DealerAddress, opt => opt.Ignore())
                .ForMember(dest => dest.DealerLandmark, opt => opt.Ignore())
                .ForMember(dest => dest.DealerGPSLocation, opt => opt.Ignore())
                .ForMember(dest => dest.DealerRating, opt => opt.Ignore())
                .ForMember(dest => dest.DealerOpenOrClosed, opt => opt.Ignore());

            // Mapping DealerDetails to DealerSignUpVM
            CreateMap<DealerDetails, DealerSignUpVM>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DealerName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.DealerEmail))
                .ForMember(dest => dest.PhoneNo, opt => opt.MapFrom(src => src.DealerPhoneNo))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.DealerPassword));

            CreateMap<DealerDetails, AuthorizedDealerLoginVM>()
                .ForMember(opt => opt.Email, dest => dest.MapFrom(src => src.DealerEmail))
                .ForMember(opt => opt.DealerID, dest => dest.MapFrom(src => src.DealerID))
                .ForMember(opt => opt.UserName, dest => dest.MapFrom(src => src.DealerName))
                .ForMember(opt => opt.Access, dest => dest.MapFrom(src => src.Rights))
                .ForMember(opt => opt.AccessToken, dest => dest.Ignore())
                .ReverseMap()
                ;

            CreateMap<UserDetails, AuthorizedLoginVM>()
               .ForMember(opt => opt.Email, dest => dest.MapFrom(src => src.Email))
               .ForMember(opt => opt.UserID, dest => dest.MapFrom(src => src.UserID))
               .ForMember(opt => opt.UserName, dest => dest.MapFrom(src => src.Name))
               .ForMember(opt => opt.Access, dest => dest.MapFrom(src => src.Rights))
               .ForMember(opt => opt.AccessToken, dest => dest.Ignore())
               .ReverseMap()
               ;

            CreateMap<VehicleDetails, Vehicle_User_VM>()
                .ForMember(opt => opt.VehicleId, dest => dest.MapFrom(src => src.VehicleId))
                .ForMember(opt => opt.VehicleName, dest => dest.MapFrom(src => src.VehicleName))
                .ForMember(opt => opt.VehicleNumber, dest => dest.MapFrom(src => src.VehicleNumber))
                .ForMember(opt => opt.VehicleImage, dest => dest.MapFrom(src => ConvertByteToString(src.VehicleImage)))
                .ForMember(opt => opt.DriverName, dest => dest.MapFrom(src => src.DriverName))
                .ForMember(opt => opt.DriverPhoneNumber, dest => dest.MapFrom(src => src.DriverPhoneNumber))
                .ForMember(opt => opt.VehicleModel, dest => dest.MapFrom(src => src.VehicleModel))
                .ForMember(opt => opt.Alternative_Phone_Number, dest => dest.MapFrom(src => src.Alternative_Phone_Number))
                ;

            //CreateMap<Vehicle_User_VM,VehicleDetails>()
            //    .ForMember(opt => opt.VehicleId, dest => dest.MapFrom(src => src.VehicleId))
            //    .ForMember(opt => opt.VehicleName, dest => dest.MapFrom(src => src.VehicleName))
            //    .ForMember(opt => opt.VehicleNumber, dest => dest.MapFrom(src => src.VehicleNumber))
            //    .ForMember(opt => opt.VehicleImage, dest => dest.MapFrom(src => ConvertFileToByte(src.VehicleImage)))
            //    .ForMember(opt => opt.DriverName, dest => dest.MapFrom(src => src.DriverName))
            //    .ForMember(opt => opt.DriverPhoneNumber, dest => dest.MapFrom(src => src.DriverPhoneNumber))
            //    .ForMember(opt => opt.VehicleModel, dest => dest.MapFrom(src => src.VehicleModel))
            //    .ForMember(opt => opt.Alternative_Phone_Number, dest => dest.MapFrom(src => src.Alternative_Phone_Number))
            //    ;

            CreateMap<VehicleVM, VehicleDetails>()
                .ForMember(opt => opt.VehicleNumber, dest => dest.MapFrom(src => src.VehicleNumber))
                .ForMember(opt => opt.VehicleName, dest => dest.MapFrom(src => src.VehicleName))
                .ForMember(opt => opt.Alternative_Phone_Number, dest => dest.MapFrom(src => src.Alternative_Phone_Number))
                .ForMember(opt => opt.DriverName, dest => dest.MapFrom(src => src.DriverName))
                .ForMember(opt => opt.DriverPhoneNumber, dest => dest.MapFrom(src => src.DriverPhoneNumber))
                .ForMember(opt => opt.VehicleImage, dest => dest.MapFrom(src => ConvertFileToByte(src.VehicleImage)))
                .ForMember(opt => opt.VehicleModel, dest => dest.MapFrom(src => src.VehicleModel))
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

            CreateMap<AuthorizedDealerLoginVM, DealerDetails>()
                .ForMember(opt => opt.DealerName, dest => dest.MapFrom(src => src.UserName))
                .ForMember(opt => opt.DealerEmail, dest => dest.MapFrom(src => src.Email))
                .ForMember(opt => opt.Rights, dest => dest.MapFrom(src => src.Access)).ReverseMap()              
                ;

            CreateMap<UserData, UserDetails>()
                .ForMember(opt => opt.Name,dest=> dest.MapFrom(src => src.Name))
                .ForMember(opt => opt.UserProfilePicture,dest=> dest.MapFrom(src => ConvertFileToByte(src.ProfilePicture)))
                .ForMember(opt => opt.Email,dest=> dest.MapFrom(src => src.Email))
                .ForMember(opt => opt.MobileNumber,dest=> dest.MapFrom(src => src.MobileNumber))
                .ForMember(opt => opt.Address,dest=> dest.MapFrom(src => src.Address))
                .ForMember(opt => opt.UserID,dest=> dest.Ignore())
                .ForMember(opt => opt.Rights,dest=> dest.Ignore())
                .ForMember(opt => opt.CreatedDate,dest=> dest.Ignore())
                .ForMember(opt => opt.Password,dest=> dest.Ignore())
                .ReverseMap()
                ;
            CreateMap<UserDetails,UserData >()
                .ForMember(opt => opt.ProfilePicture, dest => dest.MapFrom(src => ConvertByteToFromFile(src.UserProfilePicture)));
            
            CreateMap<UserDataVM, UserDetails>()
                .ForMember(opt => opt.Name,dest=> dest.MapFrom(src => src.Name))
                //.ForMember(opt => opt.UserProfilePicture,dest=> dest.MapFrom(src => ConvertFileToByte(src.ProfilePicture)))
                .ForMember(opt => opt.Email,dest=> dest.MapFrom(src => src.Email))
                .ForMember(opt => opt.MobileNumber,dest=> dest.MapFrom(src => src.MobileNumber))
                .ForMember(opt => opt.Address,dest=> dest.MapFrom(src => src.Address))
                .ForMember(opt => opt.UserID,dest=> dest.Ignore())
                .ForMember(opt => opt.Rights,dest=> dest.Ignore())
                .ForMember(opt => opt.CreatedDate,dest=> dest.Ignore())
                .ForMember(opt => opt.Password,dest=> dest.Ignore())
                .ReverseMap()
                ;

            CreateMap<UserDetails, UserDataVM>()
                .ForMember(opt => opt.ProfilePicture, dest => dest.MapFrom(src => ConvertByteToString(src.UserProfilePicture)));

        }
    }
}
