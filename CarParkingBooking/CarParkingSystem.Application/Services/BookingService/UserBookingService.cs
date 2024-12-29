using CarParkingSystem.Application.Dtos.Booking;
using CarParkingSystem.Domain.ValueObjects;
using CarParkingSystem.Infrastructure.Database.CosmosDatabase.Entities;
using CarParkingSystem.Infrastructure.Repositories.CosmosRepository;

namespace CarParkingSystem.Application.Services.BookingService
{

    public interface IUserBookingService
    {
        Task<bool> AddBooking(BookingDto booking);
    }

    public class UserBookingService: IUserBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public UserBookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<bool> AddBooking(BookingDto booking)
        {
            CarBooking carBooking = new CarBooking()
            {
                DealerId = booking.DealerId,
                CustomerId = booking.CustomerId,
                VehicleId = booking.VehicleId,
                BookingSource = BookingSources.User.ToString(),
                BookingDate = booking.BookingDate,
                GeneratedQrCode = booking.GeneratedQrCode,
                AdvanceAmount = booking.AdvanceAmount,
                BookingStatus = new Status()
                {
                    State = BookingProcessDetails.InProgress,
                    Reason = string.Empty
                }

            };

            var data = await _bookingRepository.AddBookingDetails(carBooking);
            return data;
        }
    }
}
