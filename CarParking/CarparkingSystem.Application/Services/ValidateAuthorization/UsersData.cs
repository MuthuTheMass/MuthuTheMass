// using AutoMapper;
// using CarParkingSystem.Application.Dtos.Users;
// using CarParkingSystem.Infrastructure.Database.SQLDatabase.BookingDBContext;
// using Microsoft.EntityFrameworkCore;
//
// namespace ValidateCarParkingDetails.ValidateAuthorization
// {
//
//     public interface IUserData
//     {
//         Task<bool?> UpdateUserData(UserDataDto dataDto);
//         Task<UserDataVM?> GetSingleUser(string email);
//         
//         Task<List<UserDataForDealer>> GetAllUsers();
//     }
//
//     public class UsersData : IUserData
//     {
//         private readonly CarParkingBookingDbContext dBContext;
//         private readonly IMapper mapper;
//
//         public UsersData(CarParkingBookingDbContext _dBContext, IMapper _mapper)
//         {
//             dBContext = _dBContext;
//             mapper = _mapper;
//         }
//
//
//         public async Task<bool?> UpdateUserData(UserDataDto dataDto)
//         {
//             var IsData = await dBContext.UserDetails.FirstOrDefaultAsync(d => d.Email == dataDto.Email);
//
//             if(IsData is not null)
//             {
//                 mapper.Map(dataDto,IsData);
//                 dBContext.UserDetails.Update(IsData);
//                 await dBContext.SaveChangesAsync();
//                 return true;
//             }
//             else
//             {
//                 return false;
//             }
//         }
//
//         public async Task<UserDataVM?> GetSingleUser(string email)
//         {
//             var IsData = await dBContext.UserDetails.FirstOrDefaultAsync(d => d.Email == email);
//
//             if(IsData is not null)
//             {
//                 var data = mapper.Map<UserDataVM>(IsData);
//                 return data;
//             }
//             return null;
//         }
//
//         public async Task<List<UserDataForDealer>> GetAllUsers()
//         {
//             var data = await dBContext.UserDetails.ToListAsync();
//             
//             return mapper.Map<List<UserDataForDealer>>(data);
//         }
//     }
// }

