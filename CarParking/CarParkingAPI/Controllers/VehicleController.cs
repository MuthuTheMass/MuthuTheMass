using CarparkingSystem.Application.Services.VehicleService;
using CarParkingSystem.Application.Dtos.Vehicle;
using CarParkingSystem.Application.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarParkingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;


        public VehicleController(IVehicleService vehicleService,IUserProfile user)
        {
            _vehicleService = vehicleService;

        }

        [HttpPost(nameof(AddVehicleFromUser))]
        public async Task<IActionResult> AddVehicleFromUser([FromQuery] string userEmailId ,[FromBody] VehicleDto vehicle ) 
        {
            var result = await _vehicleService.AddVehicleByUser(vehicle, userEmailId);
            if (result == true)
            {
                return Ok(result);
            }
            else if (result == false)
            {
                return UnprocessableEntity(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
