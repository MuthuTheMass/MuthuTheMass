﻿using AutoMapper;
using DatabaseMigrator.BookingDBContext;
using DatabaseMigrator.DBModel;
using CarParkingBookingVM.VM_S.Booking;

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
                if (booking?.User_ID is null || booking?.Dealer_ID is null || booking.Vehicle_Id is null ) 
                {
                    return false;
                }
                else
                {
                    var userDetail = await dbContext.UserDetails.FindAsync(booking.User_ID);
                    var vehicleDetail = await dbContext.VehicleDetails.FindAsync(booking.Vehicle_Id);
                    var dealerDetail = await dbContext.DealerDetails.FindAsync(booking.Dealer_ID);
                    if (userDetail != null && vehicleDetail != null && dealerDetail != null)
                    {
                        var data = mapper.Map<BookingDetails>(booking);
                        await dbContext.BookingDetails.AddAsync(data);
                        await dbContext.SaveChangesAsync();
                        return true;
                    }
                    return false;
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
