using CarParkingBookingDatabase.BookingDBContext;
using CarParkingBookingDatabase.DBModel;
using CarParkingBookingVM.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidateCarParkingDetails.ValidateAuthorization
{
    public interface IAuthorization
    {
        Task<bool> UpsertLoginDetials(SignUpVM? SignUpDetials);
    }

    public class Authorization : IAuthorization
    {
        private readonly CarParkingBookingDBContext dBContext;

        public Authorization(CarParkingBookingDBContext carParkingBookingDB)
        {
            dBContext = carParkingBookingDB;
        }


        public async Task<bool> UpsertLoginDetials(SignUpVM? SignUpDetials)
        {
            if(SignUpDetials is not null)
            {
                if (SignUpDetials.Password.Equals(SignUpDetials.ConfirmPassword) && SignUpDetials.MobileNumber.Length != 10 && SignUpDetials.Email.Contains("@") && SignUpDetials.Email.EndsWith(".com"))
                {
                    return await Task.FromResult(false);
                }
                else
                {
                    var duplicate = dBContext.userDetails.FirstOrDefault(v => v.MobileNumber == SignUpDetials.MobileNumber);
                    if (duplicate is null) 
                    {
                        var data = new UserDetails()
                        {
                            Name = SignUpDetials.UserName,
                            Email = SignUpDetials.Email,
                            MobileNumber = SignUpDetials.MobileNumber,
                            Password = SignUpDetials.Password,
                        };

                        await dBContext.userDetails.AddAsync(data);
                        await dBContext.SaveChangesAsync();
                    }
                    else
                    {
                        duplicate.Name = SignUpDetials.UserName;
                        duplicate.Email = SignUpDetials.Email;
                        duplicate.Password = SignUpDetials.Password;
                        
                        dBContext.userDetails.Update(duplicate);
                        await dBContext.SaveChangesAsync();
                    }

                    return await Task.FromResult(true);
                }

            }
            return await Task.FromResult(false);
        }
    }
}
