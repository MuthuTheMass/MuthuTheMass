using AutoMapper;
using CarParkingBookingVM.Enums;
using CarParkingSystem.Application.Dtos.Authorization;
using CarParkingSystem.Application.Helper.JWTToken;
using CarParkingSystem.Infrastructure.Database.SQLDatabase.DBModel;
using CarParkingSystem.Infrastructure.Repositories;


namespace CarParkingSystem.Application.Services.Authorization;

public interface IAuthorizationService
{
    Task<AuthorizedLoginDto?> UserIsAuthorized(LoginDto user);
    Task<AuthorizedLoginDto?> DealerIsAuthorized(LoginDto user);
}

public class AuthorizeService : IAuthorizationService
{
    private readonly IUserRepository _userRepository;
    private readonly IDealerRepository _dealerRepository;
    private readonly Mapper _mapper;

    public AuthorizeService(IUserRepository userRepository, IDealerRepository dealerRepository, Mapper mapper)
    {
        _userRepository = userRepository;
        _dealerRepository = dealerRepository;
        _mapper = mapper;
    }

    public async Task<AuthorizedLoginDto?> UserIsAuthorized(LoginDto user)
    {
        UserDetails? isUser = await _userRepository.GetUserByEmail(user.Email);
        if(isUser is null) return null; 

        var data = _mapper.Map<AuthorizedLoginDto>(isUser);
        var token =GenerateJwtToken.GenerateJwtTokenToAuthorize(data.UserName, new List<string> { AccessToUser.User });
        data.AccessToken = token;
        return data ;
    }

    public async Task<AuthorizedLoginDto?> DealerIsAuthorized(LoginDto user)
    {
        DealerDetails? isDealer = await _dealerRepository.GetUserByEmail(user.Email);
        if (isDealer is null) return null;
        return _mapper.Map<AuthorizedLoginDto>(isDealer);
    }
}