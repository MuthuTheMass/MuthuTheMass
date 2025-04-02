using AutoMapper;
using CarParkingBookingVM.Enums;
using CarParkingBookingVM.Login;
using CarParkingSystem.Application.Dtos.Booking;
using CarParkingSystem.Application.Dtos.Dealers;
using CarParkingSystem.Application.Services.BookingService;
using CarParkingSystem.Domain.Dtos.Dealers;
using CarParkingSystem.Domain.Entities.SQL;
using CarParkingSystem.Domain.Helper;
using CarParkingSystem.Domain.ValueObjects;
using CarParkingSystem.Infrastructure.Database.CosmosDatabase.Entities;
using CarParkingSystem.Infrastructure.Repositories.CosmosRepository;
using CarParkingSystem.Infrastructure.Repositories.SQL_Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarParkingSystem.Application.Services.DealerService;


public interface IDealerProfile
{
    Task<bool?> DealerSignUp(SignUpDto dealer);
    
    Task<DealerRecord> GetAllDealersBySearch(Filter filter);

    Task<DashboardDetailsForDealer?> GetUsersByDealer(string emailId);

    Task<bool> DealerBookingOffline(BookingDto offlineBooking);

    Task<List<RecentBookingInDealerDashBoard>> GetAllBookingsByDealerEmailId(string emailId);

}

public class DealerProfile : IDealerProfile
{
    private readonly IDealerRepository _dealerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IDealerSlotsRepository _dealerSlotsRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IUserBookingService _bookingData;
    private readonly IMapper _mapper;
    
    public DealerProfile(IDealerRepository dealerRepository, IMapper mapper,IUserRepository userRepository,IDealerSlotsRepository dealerSlotsRepository,IBookingRepository bookingRepository, IVehicleRepository vehicleRepository, IUserBookingService bookingData)
    {
        _dealerRepository = dealerRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _dealerSlotsRepository = dealerSlotsRepository;
        _bookingRepository = bookingRepository;
        _vehicleRepository = vehicleRepository;
        _bookingData = bookingData;
    }

    public async Task<bool> DealerBookingOffline(BookingDto offlineBooking)
    {
        if (offlineBooking == null || offlineBooking?.VehicleInfo?.VehicleNumber == null)
            return false;

        await _bookingData.AddBooking(offlineBooking);

        return true;
    }

    #region DealerBookingOffline() Helper Methods
    //private CustomerUserDetails MapToCustomerUserDetails(OfflineBooking offlineBooking)
    //{
    //    return new CustomerUserDetails
    //    {
    //        CustomerId = string.Empty,
    //        CustomerName = offlineBooking.FullName,
    //        CustomerMobileNumber = offlineBooking.MobileNumber,
    //        CustomerEmail = offlineBooking.Email,
    //        CustomerAddress = offlineBooking.Address,
    //        CustomerAuthorityOfIssue = offlineBooking.AuthorityOfIssue,
    //        CustomerProof = offlineBooking.Proof,
    //        CustomerProofNumber = offlineBooking.ProofNumber
    //    };
    //}

    //private async Task<UserDetails> GetOrCreateCustomerData(OfflineBooking offlineBooking, CustomerUserDetails userInfo)
    //{
    //    var customerData = await _userRepository.GetUserByEmail(offlineBooking.Email);
    //    if (customerData == null || string.IsNullOrEmpty(customerData?.Email))
    //    {
    //        var newUser = CreateUserDetails(offlineBooking);
    //        await _userRepository.CraeteNewUser(newUser);
    //        customerData = await _userRepository.GetUserByEmail(offlineBooking.Email);
    //        userInfo.CustomerId = customerData.UserID;
    //    }
    //    else
    //    {
    //        UpdateUserInfo(userInfo, customerData);
    //    }

    //    return customerData;
    //}

    //private UserDetails CreateUserDetails(OfflineBooking offlineBooking)
    //{
    //    return new UserDetails
    //    {
    //        UserID = string.Empty,
    //        Email = offlineBooking.Email,
    //        Address = offlineBooking.Address,
    //        CreatedDate = DateTiming.GetIndianTime(offlineBooking.BookingDate),
    //        MobileNumber = offlineBooking.MobileNumber,
    //        Name = offlineBooking.FullName,
    //        Password = string.Empty,
    //        Rights = AccessToUsers.User.ToString(),
    //        UserProfilePicture = null
    //    };
    //}

    //private void UpdateUserInfo(CustomerUserDetails userInfo, UserDetails customerData)
    //{
    //    userInfo.CustomerId = customerData.UserID;
    //    userInfo.CustomerAddress = customerData.Address;
    //    userInfo.CustomerMobileNumber = customerData.MobileNumber;
    //    userInfo.CustomerEmail = customerData.Email;
    //    userInfo.CustomerName = customerData.Name;
    //}

    //private CarBooking CreateCarBooking(OfflineBooking offlineBooking, DealerDetails dealerDetails, CustomerUserDetails userInfo)
    //{
    //    return new CarBooking
    //    {
    //        DealerId = dealerDetails?.DealerID,
    //        CustomerData = userInfo,
    //        VehicleInfo = new VehicleInformation
    //        {
    //            VehicleNumber = offlineBooking.VehicleNumber,
    //            VehicleModel = offlineBooking.VehicleModel
    //        },
    //        BookingSource = BookingSources.Dealer.ToString(),
    //        BookingDate = new CarBookingDates
    //        {
    //            From = DateTiming.GetIndianTime(offlineBooking.BookingDate)
    //        },
    //        CreatedDate = DateTiming.GetIndianTime(offlineBooking.BookingDate),
    //        BookingStatus = new Status
    //        {
    //            State = BookingProcessDetails.InProgress,
    //            Reason = "Booking InProgress"
    //        },
    //        IsDeleted = false,
    //        GeneratedQrCode = null,
    //        AllottedSlots = null,
    //        AdvanceAmount = null,
    //        UpdatedDate = null
    //    };
    //}

    #endregion

    public async Task<bool?> DealerSignUp(SignUpDto dealer)
    {
        if (dealer == null || 
            string.IsNullOrEmpty(dealer.Password) || 
            dealer.MobileNumber?.Length != 10 || 
            !dealer.Email?.Contains("@") == true || 
            !dealer.Email!.EndsWith(".com"))
        {
            return false;
        }

        var duplicate = await _dealerRepository.GetUserByEmail(dealer.Email);
        if (duplicate != null)
        {
            return null;
        }

        var data = _mapper.Map<DealerDetails>(dealer);
        return await _dealerRepository.CreateDealer(data);
    }  
    public async Task<DealerRecord> GetAllDealersBySearch(Filter filter)
    {
        var mapFilter = _mapper.Map<Infrastructure.DtosHelper.Filter>(filter);
        var dealers = await _dealerRepository.GetAllDealers(mapFilter);
        
        return new DealerRecord(_mapper.Map<List<DealerDto>>(dealers.Data),dealers.TotalDataCount);
    }

    public async Task<DashboardDetailsForDealer?> GetUsersByDealer(string emailId)
    {
        var data = new DashboardDetailsForDealer();
        data.NewCustomers = new List<UserDetailForDealer>();
        data.RecentBookings = new List<RecentBookingInDealerDashBoard>();
        List<UserDetailForDealer> dashbordDetaiuls= new();
        var userData = await _userRepository.GetUserDetailsForDealer(emailId);
        var dealerSlotDetails = await _dealerSlotsRepository.GetSlotsByDealerEmailId(emailId);
        userData.ForEach(element => {
            data.NewCustomers?.Add(
                new UserDetailForDealer(
                    element.Name,
                    Convert.ToBase64String(element.UserProfilePicture ?? []),
                    element.MobileNumber)
                );
        });
        if(dealerSlotDetails is not null)
        {
            data.AvailableSlots = dealerSlotDetails.Available_Slots;
            data.BookedSlots = dealerSlotDetails.Booked_Slots;
            data.TotalSlots = dealerSlotDetails.Total_Slots;
        }

        var dealerdata = await _dealerRepository.GetUserByEmail(emailId);
        if (dealerdata is not null)
        {
            var bookings = await _bookingRepository.GetBookingByDealer(dealerdata.DealerID);
            foreach (var item in bookings)
            {
                data.RecentBookings.Add(

                    new RecentBookingInDealerDashBoard(
                        item.id,
                        item.VehicleInfo?.VehicleNumber!,
                        item.CreatedDate,
                        item.BookingStatus?.State.ToString()!,
                        item.AllottedSlots!,
                        item.GeneratedQrCode!
                        )
                    );
            }
        }



        return data;
    }

    public async Task<List<RecentBookingInDealerDashBoard>?> GetAllBookingsByDealerEmailId(string emailId)
    {
        var data = new List<RecentBookingInDealerDashBoard>();
        var dealerdata = await _dealerRepository.GetUserByEmail(emailId);
        if (dealerdata is not null)
        {
            var bookings = await _bookingRepository.GetBookingByDealer(dealerdata.DealerID);
            foreach (var item in bookings)
            {
                data.Add(

                    new RecentBookingInDealerDashBoard(
                        item.id,
                        item.VehicleInfo?.VehicleNumber!,
                        item.CreatedDate,
                        item.BookingStatus?.State.ToString()!,
                        item.AllottedSlots!,
                        item.GeneratedQrCode!
                        )
                    );
            }
            return data;
        }

        return null;


    }
}