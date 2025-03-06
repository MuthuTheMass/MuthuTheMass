using AutoMapper;
using CarParkingBookingVM.Login;
using CarParkingSystem.Application.Dtos.Dealers;
using CarParkingSystem.Domain.Dtos.Dealers;
using CarParkingSystem.Domain.Entities.SQL;
using CarParkingSystem.Infrastructure.Repositories.SQL_Repository;
using System.Threading;

namespace CarParkingSystem.Application.Services.DealerService;


public interface IDealerProfile
{
    Task<bool?> DealerSignUp(SignUpDto dealer);
    
    Task<DealerRecord> GetAllDealersBySearch(Filter filter);

    Task<List<UserDetailsForDealer>> GetUsersByDealer(string emailId);
}

public class DealerProfile : IDealerProfile
{
    private readonly IDealerRepository _dealerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public DealerProfile(IDealerRepository dealerRepository, IMapper mapper,IUserRepository userRepository)
    {
        _dealerRepository = dealerRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }
    
    
    public async Task<bool?> DealerSignUp(SignUpDto dealer)
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

    public async Task<DealerRecord> GetAllDealersBySearch(Filter filter)
    {
        var mapFilter = _mapper.Map<Infrastructure.DtosHelper.Filter>(filter);
        var dealers = await _dealerRepository.GetAllDealers(mapFilter);
        
        return new DealerRecord(_mapper.Map<List<DealerDto>>(dealers.Data),dealers.TotalDataCount);
    }

    public async Task<List<UserDetailsForDealer>> GetUsersByDealer(string emailId)
    {
        var userData = await _userRepository.GetUserDetailsForDealer(emailId);
        
        return _mapper.Map<List<UserDetailsForDealer>>(userData);
    }
}