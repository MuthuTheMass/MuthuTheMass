using AutoMapper;
using CarParkingBookingDatabase.BookingDBContext;
using CarParkingBookingDatabase.DBModel;
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
            if (booking is not null) 
            {
                if (booking?.UserName is null || booking?.Dealer_Name is null || booking.Dealer_PhoneNumber is null || booking.Phone_Number is null) 
                {
                    return false;
                }
                else
                {
                    var data = mapper.Map<BookingDetails>(booking);
                    dbContext.Add(data);
                    dbContext.SaveChanges();
                    return true;
                }
            }


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
