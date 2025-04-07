using AutoMapper;
using CarParkingBookingVM.Login;
using CarParkingSystem.Application.Dtos.Users;
using CarParkingSystem.Application.Dtos.Vehicle;
using CarParkingSystem.Domain.Entities.SQL;
using CarParkingSystem.Infrastructure.Repositories.SQL_Repository;

namespace CarParkingSystem.Application.Services.UserService;

public interface IUserProfile
{
    Task<bool?> UserSignUp(SignUpDto user);

    Task<UserDataVM> GetSingleUserDetails(string emailId);
    
    Task<List<VehicleDetailOfSingle>> GetUserVehicles(string emailId);
}

public class UserProfile : IUserProfile
{
    private readonly IUserRepository _userRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IMapper _mapper;

    public UserProfile(IUserRepository userRepository, IVehicleRepository vehicleRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _vehicleRepository = vehicleRepository;
        _mapper = mapper;
    }

    public async Task<UserDataVM> GetSingleUserDetails(string emailId)
    {
        var result = await _userRepository.GetUserByEmail(emailId);
        var userDetails = _mapper.Map<UserDataVM>(result);
        var details_Of_Car = await _vehicleRepository.GetVehicleByUserId(result?.UserID);
        userDetails.carDetails = _mapper.Map<List<Vehicle_Single_User_VM>>(details_Of_Car);
        return _mapper.Map<UserDataVM>(userDetails);
    }

    public async Task<List<VehicleDetailOfSingle>> GetUserVehicles(string emailId)
    {
        var user = await _userRepository.GetUserByEmail(emailId);
        var vehicles = await _vehicleRepository.GetVehicleByUserId(user?.UserID ?? "");
        var data = vehicles.Select(x => new VehicleDetailOfSingle(
            x.VehicleId,
            x.VehicleName,
            x.VehicleNumber,
            x.VehicleModel
            )).ToList();
        
        return data;
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