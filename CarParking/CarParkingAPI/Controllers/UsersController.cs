using AutoMapper;
using CarParkingSystem.Application.Dtos.Users;
using CarParkingSystem.Application.Services.DealerService;
using CarParkingSystem.Application.Services.UserService;
using CarParkingSystem.Domain.Dtos.Dealers;
using CarParkingSystem.Infrastructure.Repositories.SQL_Repository;
using Microsoft.AspNetCore.Mvc;

namespace CarParkingBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserProfile _userData;
        private readonly IDealerProfile _dealerData;

        public UsersController(IUserProfile userData , IDealerProfile dealerData)
        {
            _userData = userData;
            _dealerData = dealerData;
        }

        [HttpGet("userfull")]
        public async Task<IActionResult> Get([FromQuery] string userEmail)
        {
            var result = await _userData.GetSingleUserDetails(userEmail);

            if (result != null)
            {
                return Ok(result);
            }
            else if (result == null)
            {
                return NotFound();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("dealersearch")]
        public async Task<IActionResult> GetDealerSearch([FromBody] Filter request )
        {
            var result = await _dealerData.GetAllDealersBySearch(request);
            return Ok(result);
        }

        [HttpGet("FullDealerDetails")]
        public async Task<IActionResult> GetFullDealerDetails([FromQuery] string dealerId)
        {
            var result = await _dealerData.GetDealerById(dealerId);
            return Ok(result);
        }

        [HttpGet("VehicleDetailsForBooking")]
        public async Task<IActionResult> GetVehicleDetailsForBooking([FromQuery] string emailId)
        {
            var result = await _userData.GetUserVehicles(emailId);
            return Ok(result);
        }
    }
}