using AutoMapper;
using CarParkingBookingVM.Login;
using CarParkingSystem.Domain.Entities.SQL;
using CarParkingSystem.Infrastructure.Repositories.SQL_Repository;
using Microsoft.EntityFrameworkCore;

namespace CarParkingSystem.Application.Services;

public interface ISignUpService
{
    Task<bool?> UserSignUp(SignUpDto user);
    Task<bool?> DealerSignUp(SignUpDto? dealer);
}

public class SignUpService : ISignUpService
{
    private readonly IUserRepository _userRepository;
    private readonly IDealerRepository _dealerRepository;
    private readonly IMapper _mapper;

    public SignUpService(IUserRepository userRepository, IDealerRepository dealerRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _dealerRepository = dealerRepository;
        _mapper = mapper;
    }


    public async Task<bool?> UserSignUp(SignUpDto user)
    {
        if (string.IsNullOrEmpty(user.Password) ||
            user.MobileNumber?.Length != 10 ||
            !user.Email?.Contains("@") == true ||
            !user.Email!.EndsWith(".com"))
        {
            return false;
        }

        var duplicate = await _userRepository.GetUserByEmail(user.Email);
        if (duplicate != null)
        {
            return null;
        }

        var data = _mapper.Map<UserDetails>(user);
        await _userRepository.CraeteNewUser(data);
        return true;
    }


public async Task<bool?> DealerSignUp(SignUpDto? dealer)
    {
        if (dealer == null || 
            string.IsNullOrEmpty(dealer.Password) || 
            dealer.MobileNumber?.Length != 10 || 
            !dealer.Email?.Contains("@") == true || 
            !dealer.Email!.EndsWith(".com"))
        {
            return false;
        }

        var duplicate = await _dealerRepository.GetUserByEmail(dealer.Email);
        if (duplicate != null)
        {
            return null;
        }

        var data = _mapper.Map<DealerDetails>(dealer);
        return await _dealerRepository.CreateDealer(data);
    }
}