using CarParkingBooking.AutoMapper.Resolver;
using CarParkingBookingVM.Enums;
using CarParkingBookingVM.Login;
using CarParkingSystem.Application.Dtos.Authorization;
using CarParkingSystem.Application.Dtos.Booking;
using CarParkingSystem.Application.Dtos.Users;
using CarParkingSystem.Application.Dtos.Vehicle;
using CarParkingSystem.Domain.Dtos.Dealers;
using CarParkingSystem.Domain.Entities.SQL;
using CarParkingSystem.Domain.Helper;
using CarParkingSystem.Infrastructure.Database.CosmosDatabase.Entities;
using System.Globalization;
using CarParkingSystem.Domain.ValueObjects;
using Dealers_Filter = CarParkingSystem.Domain.Dtos.Dealers.Filter;
using Dealers_Filters = CarParkingSystem.Domain.Dtos.Dealers.Filters;
using Filters = CarParkingSystem.Infrastructure.DtosHelper;
using VehicleDetails = CarParkingSystem.Domain.Entities.SQL.VehicleDetails;


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
                .ForMember(opt => opt.DealerID, dest => dest.MapFrom(src => src.DealerId))
                .ForMember(opt => opt.DealerEmail, dest => dest.MapFrom(src => src.DealerEmail))
                .ForMember(opt => opt.DealerDescription, dest => dest.MapFrom(src => src.DealerDescription))
                .ForMember(opt => opt.DealerProfilePicture, dest => dest.MapFrom(src => ConvertPngStringToQrByte(src.Image)))
                .ForMember(opt => opt.DealerPhoneNo, dest => dest.MapFrom(src => src.DealerPhoneNo))
                .ForMember(opt => opt.DealerTiming, dest => dest.MapFrom(src => ConvertTimingString(src.DealerTiming)))
                .ForMember(opt => opt.DealerAddress, dest => dest.MapFrom(src => src.DealerAddress))
                .ForMember(opt => opt.DealerLandmark, dest => dest.MapFrom(src => src.DealerLandmark))
                .ForMember(opt => opt.DealerCity, dest => dest.MapFrom(src => src.DealerCity))
                .ForMember(opt => opt.DealerState, dest => dest.MapFrom(src => src.DealerState))
                .ForMember(opt => opt.DealerCountry, dest => dest.MapFrom(src => src.DealerCountry))
                .ForMember(opt => opt.DealerGPSLocation, dest => dest.MapFrom(src => src.DealerLocationURL))
                .ForMember(opt => opt.DealerRating, dest => dest.MapFrom(src => src.DealerRating))
                .ForMember(opt => opt.DealerStoreName, dest => dest.MapFrom(src => src.DealerStoreName))
                .ForMember(opt => opt.OneHourAmount, dest => dest.MapFrom(src => src.OneHourAmount))
                ;

            CreateMap<DealerDetails, DealerDto>()
                .ForMember(opt => opt.DealerTiming, dest => dest.MapFrom(src => ConvertStringTiming(src.DealerTiming)))
                .ForMember(opt => opt.DealerLocationURL, dest => dest.MapFrom(src => src.DealerGPSLocation))
                .ForMember(opt => opt.Image, dest => dest.MapFrom(src => ConvertQrByteToPngString(src.DealerProfilePicture)));

            CreateMap<SignUpDto, DealerDetails>()
                .ForMember(dest => dest.DealerName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.DealerEmail, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.DealerPhoneNo, opt => opt.MapFrom(src => src.MobileNumber))
                .ForMember(dest => dest.DealerPassword, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Rights,
                    opt => opt.MapFrom(src => AccessToUsers.Dealer.ToString())) // Default value
                .ForMember(dest => dest.DealerDescription, opt => opt.Ignore())
                .ForMember(dest => dest.DealerTiming, opt => opt.Ignore())
                .ForMember(dest => dest.DealerAddress, opt => opt.Ignore())
                .ForMember(dest => dest.DealerLandmark, opt => opt.Ignore())
                .ForMember(dest => dest.DealerCity, opt => opt.Ignore())
                .ForMember(dest => dest.DealerState, opt => opt.Ignore())
                .ForMember(dest => dest.DealerCountry, opt => opt.Ignore())
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
                .ForMember(opt => opt.Alternative_Phone_Number,
                    dest => dest.MapFrom(src => src.Alternative_Phone_Number))
                ;

            CreateMap<VehicleDto, VehicleDetails>()
                .ForMember(opt => opt.VehicleNumber, dest => dest.MapFrom(src => src.VehicleNumber))
                .ForMember(opt => opt.VehicleName, dest => dest.MapFrom(src => src.VehicleName))
                .ForMember(opt => opt.Alternative_Phone_Number, dest => dest.MapFrom(src => src.Alternative_Phone_Number))
                .ForMember(opt => opt.DriverName, dest => dest.MapFrom(src => src.DriverName))
                .ForMember(opt => opt.DriverPhoneNumber, dest => dest.MapFrom(src => src.DriverPhoneNumber))
                .ForMember(opt => opt.VehicleImage,
                    dest => dest.MapFrom(src => string.IsNullOrEmpty(src.VehicleImage)
                        ? Array.Empty<byte>()
                        : Convert.FromBase64String(src.VehicleImage)))
                .ForMember(opt => opt.VehicleNumberImage,
                    dest => dest.MapFrom(src => string.IsNullOrEmpty(src.VehicleNumberImage)
                        ? Array.Empty<byte>()
                        : Convert.FromBase64String(src.VehicleNumberImage)))
                .ForMember(opt => opt.VehicleModel, dest => dest.MapFrom(src => src.VehicleModel))
                .ForMember(opt => opt.VehicleId, opt => opt.Ignore()) // or generate in service layer
                .ForMember(opt => opt.UserID, opt => opt.Ignore())
                .ForMember(opt => opt.CreatedDate, opt => opt.Ignore());


            CreateMap<VehicleDetails, VehicleDto>()
                .ForMember(dest => dest.VehicleName, opt => opt.MapFrom(src => src.VehicleName))
                .ForMember(dest => dest.VehicleNumber, opt => opt.MapFrom(src => src.VehicleNumber))
                .ForMember(dest => dest.DriverName, opt => opt.MapFrom(src => src.DriverName))
                .ForMember(dest => dest.DriverPhoneNumber, opt => opt.MapFrom(src => src.DriverPhoneNumber))
                .ForMember(dest => dest.VehicleModel, opt => opt.MapFrom(src => src.VehicleModel))
                .ForMember(dest => dest.Alternative_Phone_Number, opt => opt.MapFrom(src => src.Alternative_Phone_Number))
                .ForMember(dest => dest.VehicleImage,
                    opt => opt.MapFrom(src => src.VehicleImage != null && src.VehicleImage.Length > 0
                        ? Convert.ToBase64String(src.VehicleImage)
                        : string.Empty))
                .ForMember(dest => dest.VehicleNumberImage,
                    opt => opt.MapFrom(src => src.VehicleNumberImage != null && src.VehicleNumberImage.Length > 0
                        ? Convert.ToBase64String(src.VehicleNumberImage)
                        : string.Empty));


            CreateMap<AuthorizedLoginDto, DealerDetails>()
                .ForMember(opt => opt.DealerName, dest => dest.MapFrom(src => src.UserName))
                .ForMember(opt => opt.DealerEmail, dest => dest.MapFrom(src => src.Email))
                .ForMember(opt => opt.Rights, dest => dest.MapFrom(src => src.Access)).ReverseMap()
                ;

            CreateMap<UserDataDto, UserDetails>()
                .ForMember(opt => opt.Name, dest => dest.MapFrom(src => src.Name))
                .ForMember(opt => opt.UserProfilePicture,
                    dest => dest.MapFrom(src => ConvertFileToByte(src.ProfilePicture)))
                .ForMember(opt => opt.Email, dest => dest.MapFrom(src => src.Email))
                .ForMember(opt => opt.MobileNumber, dest => dest.MapFrom(src => src.MobileNumber))
                .ForMember(opt => opt.Address, dest => dest.MapFrom(src => src.Address))
                .ForMember(opt => opt.UserID, dest => dest.Ignore())
                .ForMember(opt => opt.Rights, dest => dest.Ignore())
                .ForMember(opt => opt.CreatedDate, dest => dest.Ignore())
                .ForMember(opt => opt.Password, dest => dest.Ignore())
                .ReverseMap()
                ;
            CreateMap<UserDetails, UserDataDto>()
                .ForMember(opt => opt.ProfilePicture,
                    dest => dest.MapFrom(src => ConvertByteToFromFile(src.UserProfilePicture)));

            CreateMap<UserDataVM, UserDetails>()
                .ForMember(opt => opt.Name, dest => dest.MapFrom(src => src.Name))
                .ForMember(opt => opt.Email, dest => dest.MapFrom(src => src.Email))
                .ForMember(opt => opt.MobileNumber, dest => dest.MapFrom(src => src.MobileNumber))
                .ForMember(opt => opt.Address, dest => dest.MapFrom(src => src.Address))
                .ForMember(opt => opt.UserID, dest => dest.Ignore())
                .ForMember(opt => opt.Rights, dest => dest.Ignore())
                .ForMember(opt => opt.CreatedDate, dest => dest.Ignore())
                .ForMember(opt => opt.Password, dest => dest.Ignore())
                .ReverseMap()
                ;

            CreateMap<UserDetails, UserDataVM>()
                .ForMember(opt => opt.ProfilePicture,
                    dest => dest.MapFrom(src => ConvertByteToString(src.UserProfilePicture)));

            CreateMap<UserDetails, UserDataForDealer>()
                .ForMember(c => c.Name, dest => dest.MapFrom(src => src.Name))
                .ForMember(c => c.Email, dest => dest.MapFrom(src => src.Email))
                .ForMember(c => c.MobileNumber, dest => dest.MapFrom(src => src.MobileNumber))
                .ReverseMap()
                ;

            CreateMap<Filters.Filter, Dealers_Filter>()
                .ForMember(opt => opt.searchFrom, dest => dest.MapFrom(src => src.searchFrom))
                .ForMember(opt => opt.filters, dest => dest.MapFrom(src => src.filters))
                .ForMember(opt => opt.pageNumber, dest => dest.MapFrom(src => src.pageNumber))
                .ForMember(opt => opt.pageSize, dest => dest.MapFrom(src => src.pageSize))
                .ReverseMap();

            CreateMap<Filters.Filters, Dealers_Filters>()
                .ForMember(opt => opt.key, dest => dest.MapFrom((src) => src.key))
                .ForMember(opt => opt.value, dest => dest.MapFrom((src) => src.value))
                .ForMember(opt => opt.fullValue, dest => dest.MapFrom(src => src.fullValue))
                .ReverseMap();

            CreateMap<CarParkingSystem.Domain.Entities.SQL.VehicleDetails, Vehicle_Single_User_VM>()
                .ForMember(opt => opt.VehicleNumber, dest => dest.MapFrom(src => src.VehicleNumber))
                .ForMember(opt => opt.VehicleName, dest => dest.MapFrom(src => src.VehicleName))
                .ForMember(opt => opt.VehicleId, dest => dest.MapFrom(src => src.VehicleId))
                .ReverseMap();

            CreateMap<UserDetailsForDealer, UserDetails>()
                .ForMember(opt => opt.Name, dest => dest.MapFrom(src => src.Name))
                .ForMember(opt => opt.UserProfilePicture, dest => dest.MapFrom(src => ConvertFileToByte(src.Picture)))
                .ForMember(opt => opt.MobileNumber, dest => dest.MapFrom(src => src.MobileNumber))
                .ReverseMap();
            CreateMap<UserDetails, UserDetailsForDealer>()
                .ForMember(opt => opt.Picture,
                    dest => dest.MapFrom(src => ConvertByteToFromFile(src.UserProfilePicture)))
                .ReverseMap();

            //CreateMap<CarBooking, OfflineBooking>()
            //    .ForMember(opt => opt.FullName, dest => dest.MapFrom(src => src.CustomerData.CustomerName))
            //    .ForMember(opt => opt.Email, dest => dest.MapFrom(src => src.CustomerData.CustomerEmail))
            //    .ForMember(opt => opt.MobileNumber, dest => dest.MapFrom(src => src.CustomerData.CustomerMobileNumber))
            //    .ForMember(opt => opt.Address, dest => dest.MapFrom(src => src.CustomerData.CustomerAddress))
            //    .ForMember(opt => opt.Proof, dest => dest.MapFrom(src => src.CustomerData.CustomerProof))
            //    .ForMember(opt => opt.ProofNumber, dest => dest.MapFrom(src => src.CustomerData.CustomerProofNumber))
            //    .ForMember(opt => opt.AuthorityOfIssue, dest => dest.MapFrom(src => src.CustomerData.CustomerAuthorityOfIssue))
            //    .ForMember(opt => opt.VehicleNumber, dest => dest.MapFrom(src => src.VehicleInfo.VehicleNumber))
            //    .ForMember(opt => opt.VehicleModel, dest => dest.MapFrom(src => src.VehicleInfo.VehicleModel))
            //    .ReverseMap();


            CreateMap<UserDetails, CustomerUserDetails>()
                .ForMember(opt => opt.CustomerId, dest => dest.MapFrom(src => src.UserID))
                .ForMember(opt => opt.CustomerName, dest => dest.MapFrom(src => src.Name))
                .ForMember(opt => opt.CustomerEmail, dest => dest.MapFrom(src => src.Email))
                .ForMember(opt => opt.CustomerMobileNumber, dest => dest.MapFrom(src => src.MobileNumber))
                .ForMember(opt => opt.CustomerAddress, dest => dest.MapFrom(src => src.Address))
                .ForMember(opt => opt.CustomerProof, dest => dest.Ignore())
                .ForMember(opt => opt.CustomerProofNumber, dest => dest.Ignore())
                .ForMember(opt => opt.CustomerAuthorityOfIssue, dest => dest.Ignore())
                .ReverseMap();

            CreateMap<CarBooking, CarBookingDetailDto>().ConvertUsing<BookingDetailsDtoResolver>();


            CreateMap<BookingDto, CarBooking>()
           .ForMember(dest => dest.CustomerData, opt => opt.MapFrom(src => src.customerDetails))
           .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.BookingId))
           .ForMember(dest => dest.VehicleInfo, opt => opt.MapFrom(src => src.VehicleInfo ?? new VehicleInformation()))
           .ForMember(dest => dest.BookingSource, opt => opt.MapFrom(src => src.BookingSource.ToString()))
           .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => src.BookingDate))
           .ForMember(dest => dest.GeneratedQrCode, opt => opt.MapFrom(src => src.GeneratedQrCode))
           .ForMember(dest => dest.AdvanceAmount, opt => opt.MapFrom(src => src.AdvanceAmount))
           .ForMember(dest => dest.BookingStatus, opt => opt.MapFrom(src => src.BookingStatus))
           .ForMember(dest => dest.DealerId, opt => opt.MapFrom(src => src.DealerId))
           .ForMember(dest => dest.AllottedSlots, opt => opt.MapFrom(src => src.AllottedSlot))
           .ForMember(dest => dest.id, opt => opt.Ignore()) // let it be generated separately
           .ForMember(dest => dest.PartitionId, opt => opt.Ignore())
           .ForMember(dest => dest.EncryptedBookingId, opt => opt.Ignore())
           .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
           .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
           .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false))
           .ReverseMap();

            CreateMap<CustomerDetails, CustomerUserDetails>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.Email ?? string.Empty))
                .ForMember(dest => dest.CustomerMobileNumber, opt => opt.MapFrom(src => src.MobileNumber))
                .ForMember(dest => dest.CustomerAddress, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerProof, opt => opt.MapFrom(src => src.Proof!.Type ?? string.Empty))
                .ForMember(dest => dest.CustomerProofNumber, opt => opt.MapFrom(src => src.Proof!.Number ?? string.Empty))
                .ForMember(dest => dest.CustomerAuthorityOfIssue, opt => opt.Ignore());

            CreateMap<CarBookingDates, CarBookingDates>().ReverseMap();
            CreateMap<Status, Status>().ReverseMap();
            CreateMap<VehicleInformation, VehicleInformation>().ReverseMap();

            CreateMap<CarBooking, BookingDto>()
                .ForMember(dest => dest.customerDetails, opt => opt.MapFrom(src => new CustomerDetails
                {
                    CustomerName = src.CustomerData.CustomerName,
                    Email = src.CustomerData.CustomerEmail,
                    MobileNumber = src.CustomerData.CustomerMobileNumber,
                    Address = src.CustomerData.CustomerAddress,
                    Proof = new Proof
                    {
                        Type = src.CustomerData.CustomerProof,
                        Number = src.CustomerData.CustomerProofNumber
                    }
                }))
                .ForMember(dest => dest.AllottedSlot, opt => opt.MapFrom(src => src.AllottedSlots))
                .ForMember(dest => dest.DealerId, opt => opt.MapFrom(src => src.DealerId))
                .ForMember(dest => dest.VehicleInfo, opt => opt.MapFrom(src => src.VehicleInfo))
                .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => src.BookingDate))
                .ForMember(dest => dest.BookingStatus, opt => opt.MapFrom(src => src.BookingStatus))
                .ForMember(dest => dest.BookingSource, opt => opt.MapFrom(src => Enum.Parse<BookingSources>(src.BookingSource)))
                .ForMember(dest => dest.AdvanceAmount, opt => opt.MapFrom(src => src.AdvanceAmount))
                .ForMember(dest => dest.GeneratedQrCode, opt => opt.MapFrom(src => src.GeneratedQrCode))
                .ForMember(dest => dest.DealerEmail, opt => opt.Ignore()) // ← optional: set it manually if needed
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerData.CustomerId))
                .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.id));

        }
    }
}