using AutoMapper;
using CarParkingSystem.Application.Dtos.Booking;
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

        Task<CarBookingDetailDto> GetSingleBookingAsync(string encryptedId);
    }

    public class UserBookingService: IUserBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private IMapper _mapper;

        public UserBookingService(IBookingRepository bookingRepository,IUserRepository userRepository,IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddBooking(BookingDto booking)
        {
            var UserDetails = await _userRepository.GetUserByEmail(booking.CustomerId ?? string.Empty);

            CarBooking carBooking = new CarBooking()
            {
                DealerId = booking.DealerId,
                CustomerData = _mapper.Map<CustomerUserDetails>(UserDetails),
                VehicleInfo = booking.VehicleInfo!,
                BookingSource = BookingSources.User.ToString(),
                BookingDate = booking.BookingDate,
                GeneratedQrCode = booking.GeneratedQrCode,
                AdvanceAmount = booking.AdvanceAmount,
                BookingStatus = new Status()
                {
                    State = BookingProcessDetails.InProgress,
                    Reason = string.Empty
                },
                AllottedSlots = booking.AllottedSlots
                
            };

            var data = await _bookingRepository.AddBookingDetails(carBooking);
            return data;
        }

        public async Task<CarBookingDetailDto> GetSingleBookingAsync(string encryptedId)
        {
            var data = await _bookingRepository.GetBookingByQR(encryptedId);
            return _mapper.Map<CarBookingDetailDto>(data);

        }

        public async Task<CarBookingDetailDto> GetSingleBookingDetialByBookingIdAsync(string bookingId)
        {
            var data = await _bookingRepository.GetSingleBooking(bookingId);
            return _mapper.Map<CarBookingDetailDto>(data);
        }
    }
}
