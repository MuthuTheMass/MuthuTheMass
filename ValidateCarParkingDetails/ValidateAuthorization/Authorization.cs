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
        Task<bool> ValidateLoginDetials(SignUpVM? SignUpDetials);
    }

    public class Authorization : IAuthorization
    {
        public Task<bool> ValidateLoginDetials(SignUpVM? SignUpDetials)
        {
            if(SignUpDetials is not null)
            {
                if (SignUpDetials.Password.Equals(SignUpDetials.ConfirmPassword) && SignUpDetials.MobileNumber.Length != 10 && SignUpDetials.Email.Contains("@") && SignUpDetials.Email.EndsWith(".com"))
                {
                    return Task.FromResult(false);
                }
                else
                {
                    //Implement add details in SQL DB

                    return Task.FromResult(true);
                }

            }
            return Task.FromResult(false);
        }
    }
}
