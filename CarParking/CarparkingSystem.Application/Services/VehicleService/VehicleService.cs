using AutoMapper;
using CarParkingSystem.Application.Dtos.Vehicle;
using CarParkingSystem.Domain.Entities.SQL;
using CarParkingSystem.Infrastructure.Repositories.SQL_Repository;

namespace CarparkingSystem.Application.Services.VehicleService
{
    public interface IVehicleService
    {
        Task<bool> AddVehicleByUser(VehicleDto vehicle,string userEmailId);
    }

    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public VehicleService(IVehicleRepository vehicleRepository,IMapper mapper,IUserRepository userRepository)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<bool> AddVehicleByUser(VehicleDto vehicle, string userEmailId)
        {
            var userInfo = await _userRepository.GetUserByEmail(userEmailId);
            var VehicleDetail = _mapper.Map<VehicleDetails>(vehicle);
            VehicleDetail.UserID = userInfo?.UserID ?? "";
            var data = await _vehicleRepository.AddVehicle(VehicleDetail);
            return data;
        }
    }
}
