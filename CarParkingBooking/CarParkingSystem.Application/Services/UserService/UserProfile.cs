using AutoMapper;
using CarParkingBookingVM.Login;
using CarParkingSystem.Infrastructure.Database.SQLDatabase.DBModel;
using CarParkingSystem.Infrastructure.Repositories;

namespace CarParkingSystem.Application.Services.UserService;


public interface IUserProfile
{
    Task<bool?> UserSignUp(SignUpDto user);
}

public class UserProfile : IUserProfile
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UserProfile(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
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
    
}