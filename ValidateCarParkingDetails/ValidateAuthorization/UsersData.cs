using AutoMapper;
using CarParkingBookingDatabase.BookingDBContext;
using Microsoft.EntityFrameworkCore;
using CarParkingBookingVM.VM_S;

namespace ValidateCarParkingDetails.ValidateAuthorization
{

    public interface IUserData
    {
        Task<bool?> UpdateUserData(UserData data);
        Task<UserDataVM?> GetSingleUser(string email);
    }

    public class UsersData : IUserData
    {
        private readonly CarParkingBookingDBContext dBContext;
        private readonly IMapper mapper;

        public UsersData(CarParkingBookingDBContext _dBContext, IMapper _mapper)
        {
            dBContext = _dBContext;
            mapper = _mapper;
        }


        public async Task<bool?> UpdateUserData(UserData data)
        {
            var IsData = await dBContext.UserDetails.FirstOrDefaultAsync(d => d.Email == data.Email);

            if(IsData is not null)
            {
                mapper.Map(data,IsData);
                dBContext.UserDetails.Update(IsData);
                await dBContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<UserDataVM?> GetSingleUser(string email)
        {
            var IsData = await dBContext.UserDetails.FirstOrDefaultAsync(d => d.Email == email);

            if(IsData is not null)
            {
                var data = mapper.Map<UserDataVM>(IsData);
                return data;
            }
            return null;
        }
    }
}
