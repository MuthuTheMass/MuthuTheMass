using CarParkingBookingVM.VM_S.Vehicle;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidateCarParkingDetails.ValidateAuthorization;

namespace CarParkingBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleData vehicleData; 

        public VehicleController(IVehicleData vehicle)
        {
            vehicleData = vehicle;
        }

        [HttpGet("")]
        public IActionResult GetVehicleDetails_UserId([FromQuery] string UserId) 
        {
            var result = vehicleData.GetVehicleDetailsBy_UserID(UserId);
            if ( result.Result?.Count > 0) 
            {
                return Ok(result);

            }
            else if(result is null || result.Result?.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return BadRequest(result);
            }

        }

        [HttpGet("vehiclesingle")]
        public IActionResult vehicleDetailsBySingle([FromQuery] string UserId, [FromQuery] string VehileId)
        {
            var result = vehicleData.GetVehicleDetailsSingle(UserId,VehileId);
            if(!string.IsNullOrEmpty(result.Result?.VehicleId))
            {
                return Ok(result);
            }
            else if (result is null)
            {
                return NotFound();
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("addvehicle")]
        public IActionResult UpsertVehicleDetails([FromQuery] string userId, [FromForm]VehicleVM vehicleDetails)
        {
            var result = vehicleData.UpsertVehicle(userId,vehicleDetails);
            if (result is null)
            {
                return NotFound();
            }
            else if(result.Result is true)
            {
                return Ok("Vehicle updated successfully");
            }
            else if(result.Result is false) 
            {
            
                return Ok("Vehicle added successfully.");
            }
            else
            {
                return BadRequest();
            }
        }


    }
}
