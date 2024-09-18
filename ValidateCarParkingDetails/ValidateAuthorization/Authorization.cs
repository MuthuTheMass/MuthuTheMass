using AutoMapper;
using CarParkingBookingDatabase.BookingDBContext;
using CarParkingBookingDatabase.DBModel;
using CarParkingBookingVM.Authorization;
using CarParkingBookingVM.Login;

namespace ValidateCarParkingDetails.ValidateAuthorization
{
    public interface IAuthorization
    {
        Task<bool> UpsertLoginDetials(SignUpVM? SignUpDetials);

        Task<AuthorizedLoginVM> VerifyUser(LoginVM login);

        Task<bool> CheckUserAlreadyExists(string Email);
    }

    public class Authorization : IAuthorization
    {
        private readonly CarParkingBookingDBContext dBContext;
        private readonly IMapper mapper;

        public Authorization(CarParkingBookingDBContext carParkingBookingDB,IMapper _mapper)
        {
            dBContext = carParkingBookingDB;
            mapper = _mapper;
        }

        public async Task<bool> UpsertLoginDetials(SignUpVM? SignUpDetials)
        {
            if(SignUpDetials is not null) 
            {
                if (!(SignUpDetials.Password.Equals(SignUpDetials.ConfirmPassword))
                    || !(SignUpDetials.MobileNumber.Length == 10)
                    || !SignUpDetials.Email.Contains("@")
                    || !SignUpDetials.Email.EndsWith(".com"))
                {
                    return await Task.FromResult(false);
                }
                else
                {
                    var duplicate = dBContext.userDetails.FirstOrDefault(v => v.Email == SignUpDetials.Email);
                    if (duplicate is null) 
                    {
                        var data =mapper.Map<UserDetails>(SignUpDetials);

                        await dBContext.userDetails.AddAsync(data);
                        await dBContext.SaveChangesAsync();
                    }
                    else
                    {
                        mapper.Map(SignUpDetials, duplicate);
                        dBContext.userDetails.Update(duplicate);
                        await dBContext.SaveChangesAsync();
                    }

                    return await Task.FromResult(true);
                }

            }
            return await Task.FromResult(false);
        }

        public Task<AuthorizedLoginVM> VerifyUser(LoginVM login)
        {
            if(login.Email is not null && login.Password is not null)
            {
                var data = dBContext.userDetails.FirstOrDefault(y =>y.Email == login.Email);
                if (data is not null && data.Password.Equals(login.Password))
                {
                    var result = new AuthorizedLoginVM()
                    {
                        Email = data.Email,
                        Access = data.Rights
                    };

                    return Task.FromResult(result);
                }
            }
            return Task.FromResult(new AuthorizedLoginVM());
        }

        public Task<bool> CheckUserAlreadyExists(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                var data = dBContext.userDetails.Where(y => y.Email == Email).Select(v=>v.Email).ToList();
            }
        }
    }
}
