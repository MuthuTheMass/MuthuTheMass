using AutoMapper;
using CarParkingBookingVM.Login;
using CarParkingSystem.Application.Dtos.Dealers;
using CarParkingSystem.Infrastructure.Database.SQLDatabase.DBModel;
using CarParkingSystem.Infrastructure.Repositories;

namespace CarParkingSystem.Application.Services.DealerService;


public interface IDealerProfile
{
    Task<bool?> DealerSignUp(SignUpDto dealer);
    
    Task<List<DealerDto>> GetAllDealersBySearch(Filters filter);
}

public class DealerProfile : IDealerProfile
{
    private readonly IDealerRepository _dealerRepository;
    private readonly IMapper _mapper;
    
    public DealerProfile(IDealerRepository dealerRepository, IMapper mapper)
    {
        _dealerRepository = dealerRepository;
        _mapper = mapper;
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

    public async Task<List<DealerDto>> GetAllDealersBySearch(Dtos.Dealers.Filters filter)
    {
        var mapFilter = _mapper.Map<Infrastructure.DtosHelper.Filters>(filter);
        List<DealerDetails> dealers = await _dealerRepository.GetAllDealers(mapFilter);
        return _mapper.Map<List<DealerDto>>(dealers);
    }
}