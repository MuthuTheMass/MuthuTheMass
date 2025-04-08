// using AutoMapper;
// using CarParkingSystem.Application.Dtos.Booking;
// using CarParkingSystem.Domain.Entities.SqlDatabase.DBModel;
// using CarParkingSystem.Infrastructure.Database.SQLDatabase.BookingDBContext;
// using CarParkingSystem.Infrastructure.Database.SQLDatabase.DBModel;
//
// namespace ValidateCarParkingDetails.ValidateAuthorization
// {
//     public interface IBookingData
//     {
//         Task<bool> AddBooking(BookingDto booking);
//
//         Task<bool> RemoveBooking(BookingDto booking);
//
//         Task<bool> SearchBooking(BookingDto booking);
//     }
//
//
//     public class BookingData : IBookingData
//     {
//
//         private readonly CarParkingBookingDbContext dbContext;
//         private readonly IMapper mapper;
//
//         public BookingData(CarParkingBookingDbContext _dbContext,IMapper _mapper)
//         {
//             dbContext = _dbContext;
//             mapper = _mapper;
//         }
//
//         public async Task<bool> AddBooking(BookingDto booking)
//         {
//             if (booking is not null) 
//             {
//                 if (booking?.User_ID is null || booking?.Dealer_ID is null || booking.Vehicle_Id is null ) 
//                 {
//                     return false;
//                 }
//                 else
//                 {
//                     var userDetail = await dbContext.UserDetails.FindAsync(booking.User_ID);
//                     var vehicleDetail = await dbContext.VehicleDetails.FindAsync(booking.Vehicle_Id);
//                     var dealerDetail = await dbContext.DealerDetails.FindAsync(booking.Dealer_ID);
//                     if (userDetail != null && vehicleDetail != null && dealerDetail != null)
//                     {
//                         var data = mapper.Map<BookingDetails>(booking);
//                         await dbContext.BookingDetails.AddAsync(data);
//                         await dbContext.SaveChangesAsync();
//                         return true;
//                     }
//                     return false;
//                 }
//             }
//
//
//             return false;
//         }
//
//         public Task<bool> RemoveBooking(BookingDto booking)
//         {
//             throw new NotImplementedException();
//         }
//
//         public Task<bool> SearchBooking(BookingDto booking)
//         {
//             throw new NotImplementedException();
//         }
//     }
// }

