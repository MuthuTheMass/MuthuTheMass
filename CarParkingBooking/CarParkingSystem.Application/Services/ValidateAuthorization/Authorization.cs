using CarParkingBookingVM.Login;
using CarParkingSystem.Application.Dtos.Authorization;
using CarParkingSystem.Application.Dtos.Dealers;


namespace CarParkingSystem.Application.Services.ValidateAuthorization
{
    public interface IAuthorization 
    {
        Task<bool?> InsertLoginDetials(SignUpDto? SignUpDetials);

        Task<AuthorizedLoginDto> VerifyUser(LoginDto login);
        Task<bool?> InsertDealerDetails(DealerSignUpDto dealerSign);
        Task<(AuthorizedDealerLoginDto, bool?)> VerifyDealer(DealerLogin dealer);


    }

    public class Authorization : IAuthorization
    {
        private readonly CarParkingBookingDbContext dBContext;
        private readonly IMapper mapper;

        public Authorization(CarParkingBookingDbContext carParkingBookingDB,IMapper _mapper)
        {
            dBContext = carParkingBookingDB;
            mapper = _mapper;
        }

        // User Authorization

        public async Task<bool?> InsertLoginDetials(SignUpDto? SignUpDetials)
        {
            if(SignUpDetials is not null) 
            {
                if (string.IsNullOrEmpty(SignUpDetials.Password!)
                    || !(SignUpDetials.MobileNumber!.Length == 10)
                    || !SignUpDetials.Email!.Contains("@")
                    || !SignUpDetials.Email.EndsWith(".com"))
                {
                    return await Task.FromResult(false);
                }
                else
                {
                    var duplicate = dBContext.UserDetails.FirstOrDefault(v => v.Email == SignUpDetials.Email);
                    if (duplicate is null) 
                    {
                        var data =mapper.Map<UserDetails>(SignUpDetials);
                        dBContext.UserDetails.Add(data);
                        dBContext.Entry(data).State = EntityState.Added;
                        dBContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return null;
                    }

                }

            }
            return await Task.FromResult(false);
        }

        public Task<AuthorizedLoginDto> VerifyUser(LoginDto login)
        {
            if(login.Email is not null && login.Password is not null)
            {
                var data = dBContext.UserDetails.FirstOrDefault(y =>y.Email == login.Email);
                if (data is not null && data.Password.Equals(login.Password))
                {
                    var result = mapper.Map<AuthorizedLoginDto>(data);


                    return Task.FromResult(result);
                }
            }
            return Task.FromResult<AuthorizedLoginDto>(null!);
        }

        //dealer Authorization

        public async Task<bool?> InsertDealerDetails(DealerSignUpDto dealerSign)
        {
            if (dealerSign is not null)
            {
                if (string.IsNullOrEmpty(dealerSign.Password!)
                    || !(dealerSign.PhoneNo!.Length == 10)
                    || !dealerSign.Email!.Contains("@")
                    || !dealerSign.Email.EndsWith(".com"))
                {
                    return await Task.FromResult(false);
                }
                else
                {
                    var duplicate = dBContext.DealerDetails.FirstOrDefault(v => v.DealerEmail == dealerSign.Email);
                    if (duplicate is null)
                    {
                        var data = mapper.Map<DealerDetails>(dealerSign);

                        await dBContext.DealerDetails.AddAsync(data);
                        await dBContext.SaveChangesAsync();
                    }
                    else
                    {
                        return null;
                    }

                    return await Task.FromResult(true);
                }

            }

            return false;
        }

        public async Task<(AuthorizedDealerLoginDto, bool?)> VerifyDealer(DealerLogin dealer)
        {
            var data = dBContext.DealerDetails.FirstOrDefault(h=>h.DealerEmail == dealer.Email);
            if (data is null)
            {
                return (null,null);
            }
            else if (data.DealerPassword != dealer.Password) 
            {
                return (null,false);
            }
            else
            {
                return (mapper.Map<AuthorizedDealerLoginDto>(data),null);
                
            }
        }
    }
}
