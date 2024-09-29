using AutoMapper;
using CarParkingBookingDatabase.BookingDBContext;
using CarParkingBookingVM.VM_S.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidateCarParkingDetails.ValidateAuthorization
{
    public interface IBookingData
    {
        Task<bool> AddBooking(BookingVM booking);

        Task<bool> RemoveBooking(BookingVM booking);

        Task<bool> SearchBooking(BookingVM booking);
    }


    public class BookingData : IBookingData
    {

        private readonly CarParkingBookingDBContext dbContext;
        private readonly IMapper mapper;

        public BookingData(CarParkingBookingDBContext _dbContext,IMapper _mapper)
        {
            dbContext = _dbContext;
            mapper = _mapper;
        }

        public async Task<bool> AddBooking(BookingVM booking)
        {
            return false;
        }

        public Task<bool> RemoveBooking(BookingVM booking)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SearchBooking(BookingVM booking)
        {
            throw new NotImplementedException();
        }
    }
}
