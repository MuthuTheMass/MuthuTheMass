using AutoMapper;
using CarParkingBookingVM.Enums;
using CarParkingSystem.Application.Dtos.Authorization;
using CarParkingSystem.Application.Helper.JWTToken;
using CarParkingSystem.Domain.Entities.SQL;
using CarParkingSystem.Infrastructure.Repositories.SQL_Repository;


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
    private readonly IMapper _mapper;

    public AuthorizeService(IUserRepository userRepository, IDealerRepository dealerRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _dealerRepository = dealerRepository;
        _mapper = mapper;
    }

    public async Task<AuthorizedLoginDto?> UserIsAuthorized(LoginDto user)
    {
        UserDetails? userAvailable = await _userRepository.GetUserByEmail(user.Email);
        if(userAvailable is null) return null;

        var data = _mapper.Map<AuthorizedLoginDto>(userAvailable);
        var token =GenerateJwtToken.GenerateJwtTokenToAuthorize(data.UserName, new List<string> { AccessToUser.User });
        data.AccessToken = token;
        return data ;
    }

    public async Task<AuthorizedLoginDto?> DealerIsAuthorized(LoginDto user)
    {
        DealerDetails? dealerAvailable = await _dealerRepository.GetUserByEmail(user.Email);
        if (dealerAvailable is null) return null;
        
        AuthorizedLoginDto dealerLogin = _mapper.Map<AuthorizedLoginDto>(dealerAvailable);
        var token = GenerateJwtToken.GenerateJwtTokenToAuthorize(dealerLogin.UserName, new List<string> { AccessToUser.User });
        dealerLogin.AccessToken = token;
        return dealerLogin;
    }
}