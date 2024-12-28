using CarParkingBookingVM.Enums;
using CarParkingBookingVM.Login;
using CarParkingSystem.Application.Dtos.Authorization;
using CarParkingSystem.Application.Dtos.Dealers;
using CarParkingSystem.Application.Dtos.Users;
using CarParkingSystem.Application.Dtos.Vehicle;
using CarParkingSystem.Domain.Entities.SQL;
using CarParkingSystem.Infrastructure.DtosHelper;
using Filter = CarParkingSystem.Application.Dtos.Dealers;
using Filters = CarParkingSystem.Infrastructure.DtosHelper;


namespace CarParkingBooking.AutoMapper
{
    public class MapperProfile : MapperHelper
    {

        public MapperProfile()
        {
            CreateMap<UserDetails, SignUpDto>()
                .ForMember(opt => opt.UserName, dest => dest.MapFrom(src => src.Name))
                .ForMember(opt => opt.Email, dest => dest.MapFrom(src => src.Email))
                .ForMember(opt => opt.MobileNumber, dest => dest.MapFrom(src => src.MobileNumber))
                .ForMember(opt => opt.Password, dest => dest.MapFrom(src => src.Password))
                .ReverseMap();

            CreateMap<DealerDto, DealerDetails>()
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

            CreateMap<DealerDetails, DealerDto>()
                .ForMember(opt => opt.DealerTiming, dest => dest.MapFrom(src => ConvertStringTiming(src.DealerTiming)))
                .ForMember(opt => opt.DealerLocationURL, dest => dest.MapFrom(src => src.DealerGPSLocation));


            //CreateMap<BookingVM, BookingDetails>()
            //    .ForMember(opt => opt.RC_Book_File, dest => dest.MapFrom(src => convertFileToByte(src.RC_Book_File)))
            //    .ForMember(opt => opt.Vehicle_Image, dest => dest.MapFrom(src => convertFileToByte(src.Vehicle_Image)))
            //    ;

            CreateMap<SignUpDto, DealerDetails>()
                .ForMember(dest => dest.DealerName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.DealerEmail, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.DealerPhoneNo, opt => opt.MapFrom(src => src.MobileNumber))
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
            CreateMap<DealerDetails, SignUpDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.DealerName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.DealerEmail))
                .ForMember(dest => dest.MobileNumber, opt => opt.MapFrom(src => src.DealerPhoneNo))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.DealerPassword));

            CreateMap<DealerDetails, AuthorizedLoginDto>()
                .ForMember(opt => opt.Email, dest => dest.MapFrom(src => src.DealerEmail))
                .ForMember(opt => opt.ID, dest => dest.MapFrom(src => src.DealerID))
                .ForMember(opt => opt.UserName, dest => dest.MapFrom(src => src.DealerName))
                .ForMember(opt => opt.Access, dest => dest.MapFrom(src => src.Rights))
                .ForMember(opt => opt.AccessToken, dest => dest.Ignore())
                .ReverseMap()
                ;

            CreateMap<UserDetails, AuthorizedLoginDto>()
               .ForMember(opt => opt.Email, dest => dest.MapFrom(src => src.Email))
               .ForMember(opt => opt.ID, dest => dest.MapFrom(src => src.UserID))
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

            CreateMap<VehicleDto, VehicleDetails>()
                .ForMember(opt => opt.VehicleNumber, dest => dest.MapFrom(src => src.VehicleNumber))
                .ForMember(opt => opt.VehicleName, dest => dest.MapFrom(src => src.VehicleName))
                .ForMember(opt => opt.Alternative_Phone_Number, dest => dest.MapFrom(src => src.Alternative_Phone_Number))
                .ForMember(opt => opt.DriverName, dest => dest.MapFrom(src => src.DriverName))
                .ForMember(opt => opt.DriverPhoneNumber, dest => dest.MapFrom(src => src.DriverPhoneNumber))
                .ForMember(opt => opt.VehicleImage, dest => dest.MapFrom(src => ConvertFileToByte(src.VehicleImage)))
                .ForMember(opt => opt.VehicleModel, dest => dest.MapFrom(src => src.VehicleModel))
                ;

            CreateMap<VehicleDetails, VehicleDto>()
                .ForMember(opt => opt.VehicleNumber, dest => dest.MapFrom(src => src.VehicleNumber))
                .ForMember(opt => opt.VehicleName, dest => dest.MapFrom(src => src.VehicleName))
                .ForMember(opt => opt.Alternative_Phone_Number, dest => dest.MapFrom(src => src.Alternative_Phone_Number))
                .ForMember(opt => opt.DriverName, dest => dest.MapFrom(src => src.DriverName))
                .ForMember(opt => opt.DriverPhoneNumber, dest => dest.MapFrom(src => src.DriverPhoneNumber))
                .ForMember(opt => opt.VehicleImage, dest => dest.MapFrom(src => ConvertByteToFromFile(src.VehicleImage)))
                .ForMember(opt => opt.VehicleModel, dest => dest.MapFrom(src => src.VehicleModel))
                ;

            CreateMap<AuthorizedLoginDto, DealerDetails>()
                .ForMember(opt => opt.DealerName, dest => dest.MapFrom(src => src.UserName))
                .ForMember(opt => opt.DealerEmail, dest => dest.MapFrom(src => src.Email))
                .ForMember(opt => opt.Rights, dest => dest.MapFrom(src => src.Access)).ReverseMap()              
                ;

            CreateMap<UserDataDto, UserDetails>()
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
            CreateMap<UserDetails,UserDataDto >()
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

            CreateMap<UserDetails, UserDataForDealer>()
                .ForMember(c => c.Name, dest => dest.MapFrom(src => src.Name))
                .ForMember(c => c.Email, dest => dest.MapFrom(src => src.Email))
                .ForMember(c => c.MobileNumber, dest => dest.MapFrom(src => src.MobileNumber))
                .ReverseMap()
                ;

            CreateMap<Filters.Filter,Filter.Filter>()
                .ForMember(opt => opt.searchFrom,dest => dest.MapFrom(src => src.searchFrom))
                .ForMember(opt => opt.filters,dest => dest.MapFrom(src => src.filters))
                .ReverseMap();

            CreateMap<Filters.Filters, Filter.Filters>()
                .ForMember(opt => opt.key, dest => dest.MapFrom((src) => src.key))
                .ForMember(opt => opt.value, dest => dest.MapFrom((src) => src.value))
                .ForMember(opt => opt.fullValue,dest =>dest.MapFrom(src => src.fullValue))
                .ReverseMap();

            CreateMap<VehicleDetails,Vehicle_Single_User_VM>()
                .ForMember(opt => opt.VehicleNumber,dest => dest.MapFrom(src => src.VehicleNumber))
                .ForMember(opt => opt.VehicleName,dest => dest.MapFrom(src => src.VehicleName))
                .ForMember(opt => opt.VehicleId,dest => dest.MapFrom(src => src.VehicleId))
                .ReverseMap();
        }
    }
}
