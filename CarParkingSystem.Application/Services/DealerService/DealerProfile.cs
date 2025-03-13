using AutoMapper;
using CarParkingBookingVM.Login;
using CarParkingSystem.Application.Dtos.Dealers;
using CarParkingSystem.Domain.Dtos.Dealers;
using CarParkingSystem.Domain.Entities.SQL;
using CarParkingSystem.Infrastructure.Repositories.CosmosRepository;
using CarParkingSystem.Infrastructure.Repositories.SQL_Repository;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using System.Threading;

namespace CarParkingSystem.Application.Services.DealerService;


public interface IDealerProfile
{
    Task<bool?> DealerSignUp(SignUpDto dealer);
    
    Task<DealerRecord> GetAllDealersBySearch(Filter filter);

    Task<DashboardDetailsForDealer?> GetUsersByDealer(string emailId);
}

public class DealerProfile : IDealerProfile
{
    private readonly IDealerRepository _dealerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IDealerSlotsRepository _dealerSlotsRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IMapper _mapper;
    
    public DealerProfile(IDealerRepository dealerRepository, IMapper mapper,IUserRepository userRepository,IDealerSlotsRepository dealerSlotsRepository,IBookingRepository bookingRepository,IVehicleRepository vehicleRepository)
    {
        _dealerRepository = dealerRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _dealerSlotsRepository = dealerSlotsRepository;
        _bookingRepository = bookingRepository;
        _vehicleRepository = vehicleRepository; 
    }
    
    
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
            var vehicleDetails = await _vehicleRepository.GetVehicleById(dealerdata.DealerID);
            foreach (var item in bookings)
            {
                data.RecentBookings.Add(

                    new RecentBookingInDealerDashBoard(
                        item.id,
                        vehicleDetails?.VehicleId,
                        DateTime.Parse(item.CreatedDate),
                        item.BookingStatus.State.ToString(),
                        item.AllottedSlots,
                        item.GeneratedQrCode
                        )
                    );
            }
        }



        return data;
    }
}