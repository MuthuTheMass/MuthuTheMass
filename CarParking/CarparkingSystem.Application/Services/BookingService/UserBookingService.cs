using AutoMapper;
using CarParkingSystem.Application.Dtos.Booking;
using CarParkingSystem.Application.Helper.DtoHelper;
using CarParkingSystem.Domain.Entities.SQL;
using CarParkingSystem.Domain.Helper;
using CarParkingSystem.Domain.ValueObjects;
using CarParkingSystem.Infrastructure.Database.CosmosDatabase.Entities;
using CarParkingSystem.Infrastructure.Repositories.CosmosRepository;
using CarParkingSystem.Infrastructure.Repositories.SQL_Repository;

namespace CarParkingSystem.Application.Services.BookingService
{
    public interface IUserBookingService
    {
        Task<bool> AddBooking(BookingDto booking);

        Task<CarBookingDetailDto> GetSingleBookingDetialByBookingIdAsync(string bookingId);
        Task<PreUserBookingDetails> GetFirstBookingDetialByBookingIdAsync(string bookingId);

        Task<CarBookingDetailDto> GetSingleBookingAsync(string encryptedId);
        
        Task<BookingDto> GetSingleBookingAsync(DateTime date,string customerEmail);

        Task<List<UserBookingHistory>> GetUserBookingHistoryAsync(string emailId);
        Task<bool> ProcessPaymentForUserBooking();
    }

    public class UserBookingService : IUserBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDealerRepository _dealerRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private IMapper _mapper;

        public UserBookingService(IBookingRepository bookingRepository, IUserRepository userRepository, IMapper mapper,
            IDealerRepository DealerRepository, IVehicleRepository vehicleRepository)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _dealerRepository = DealerRepository;
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddBooking(BookingDto booking)
        {
            var UserDetails = await _userRepository.GetUserByEmail(booking.CustomerId ?? string.Empty);
            var DealerDetails = await _dealerRepository.GetUserByEmail(booking?.DealerEmail ?? string.Empty);

            if (UserDetails == null)
            {
                UserDetails = new UserDetails()
                {
                    UserID = string.Empty,
                    Email = booking.CustomerId ?? string.Empty,
                    Name = booking.customerDetails.CustomerName ?? string.Empty,
                    MobileNumber = string.Empty,
                    Password = string.Empty,
                };
            }

            if (DealerDetails is null)
            {
                DealerDetails = new DealerDetails()
                {
                    DealerEmail = string.Empty,
                    DealerID = string.Empty,
                    DealerName = string.Empty,
                    DealerPhoneNo = string.Empty,
                    DealerPassword = string.Empty
                };
            }
            else
            {
                booking.DealerId = DealerDetails.DealerID;
                booking.DealerEmail = DealerDetails.DealerEmail;
            }


            CarBooking carBooking = new CarBooking()
            {
                DealerId = booking.DealerId,
                CustomerData = _mapper.Map<CustomerUserDetails>(UserDetails),
                VehicleInfo = booking.VehicleInfo!,
                BookingSource = booking.BookingSource.ToString(),
                BookingDate = booking.BookingDate,
                GeneratedQrCode = booking.GeneratedQrCode,
                Payment = new PaymentInfo()
                {
                    Source = BookingSources.Dealer,
                    CreatedDate = DateTiming.GetIndianTime(),
                },
                BookingStatus = new Status()
                {
                    State = BookingProcessDetails.InProgress,
                    Reason = string.Empty
                },
                AllottedSlots = booking.AllottedSlot
            };

            var data = await _bookingRepository.AddBookingDetails(carBooking);
            return data;
        }

        public async Task<PreUserBookingDetails> GetFirstBookingDetialByBookingIdAsync(string bookingId)
        {
            var data = await _bookingRepository.GetSingleBooking(bookingId);
            var dealerInfo = await _dealerRepository.GetDealerById(data.DealerId);
            var vehicleInfo = await _vehicleRepository.GetVehicleByNumber(data.VehicleInfo.VehicleNumber);
            data.VehicleInfo.VehicleImage = $"data:image/png;base64,{Convert.ToBase64String(vehicleInfo?.VehicleImage)}" ?? null;
            var convertedData = Dtos_Helper.converter(data, vehicleInfo, dealerInfo);
            return convertedData;
        }

        public async Task<CarBookingDetailDto> GetSingleBookingAsync(string encryptedId)
        {
            var data = await _bookingRepository.GetBookingByQR(encryptedId);
            return _mapper.Map<CarBookingDetailDto>(data);
        }

        public async Task<BookingDto> GetSingleBookingAsync(DateTime date, string customerEmail)
        {
            var data = await _bookingRepository.GetSingleBookingByDate(date, customerEmail);
            var vehicleInfo = await _vehicleRepository.GetVehicleByNumber(data.VehicleInfo.VehicleNumber);
            data.VehicleInfo.VehicleImage = $"data:image/png;base64,{Convert.ToBase64String(vehicleInfo?.VehicleImage)}";
            return _mapper.Map<BookingDto>(data);
        }

        public async Task<CarBookingDetailDto> GetSingleBookingDetialByBookingIdAsync(string bookingId)
        {
            var data = await _bookingRepository.GetSingleBooking(bookingId);
            var vehicleInfo = await _vehicleRepository.GetVehicleByNumber(data.VehicleInfo.VehicleNumber);
            data.VehicleInfo.VehicleImage = $"data:image/png;base64,{Convert.ToBase64String(vehicleInfo?.VehicleImage)}" ?? null ;
            return  _mapper.Map<CarBookingDetailDto>(data);
        }

        public async Task<List<UserBookingHistory>> GetUserBookingHistoryAsync(string emailId)
        {
            var data = await _bookingRepository.GetBookingByUser(emailId);

            // Sort based on the numeric part of the Booking ID
            var sortedData =  data.OrderByDescending(b => Dtos_Helper.ExtractBookingNumber(b.id)).ToList();

            List<UserBookingHistory> convertedBookingHistory = new();

            foreach (var booking in sortedData)
            {
                var dealer = await _dealerRepository.GetDealerById(booking.DealerId ?? "");

                convertedBookingHistory.Add(
                    new UserBookingHistory(booking.id, // booking.id is already "Booking-1", etc.
                        booking.VehicleInfo.VehicleNumber,
                        booking.CreatedDate,
                        booking.BookingStatus.State,
                        booking.AllottedSlots ?? "",
                        dealer?.DealerStoreName ?? "")
                );
            }

            return convertedBookingHistory;
        }

        public async Task<bool> ProcessPaymentForUserBooking()
        {
            return false;
        }
    }
}