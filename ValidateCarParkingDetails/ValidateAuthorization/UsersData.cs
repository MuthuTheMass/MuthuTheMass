using AutoMapper;
using CarParkingBookingDatabase.BookingDBContext;
using CarParkingBookingDatabase.DBModel;
using CarParkingBookingVM.VM_S;
using Microsoft.EntityFrameworkCore;

namespace ValidateCarParkingDetails.ValidateAuthorization
{

    public interface IUserData
    {
        Task<bool?> UpdateUserData(UserUpdateDetails data); 
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


        public async Task<bool?> UpdateUserData(UserUpdateDetails data)
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
    }
}
