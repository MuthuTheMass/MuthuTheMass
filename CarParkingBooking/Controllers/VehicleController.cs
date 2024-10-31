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
        public async Task<IActionResult> GetVehicleDetails_UserId([FromQuery] string UserId) 
        {
            var result = await vehicleData.GetVehicleDetailsBy_UserID(UserId, false);
            if ( result?.Count > 0) 
            {
                return Ok(result);

            }
            else if(result?.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return BadRequest(result);
            }

        }
        
        [HttpGet("halfvehicledetails")]
        public async Task<IActionResult> GetHalfVehicleDetails_UserId([FromQuery] string UserId) 
        {
            var result = await vehicleData.GetVehicleDetailsBy_UserID(UserId, true);
            if ( result?.Count > 0) 
            {
                return Ok(result);

            }
            else if(result?.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return BadRequest(result);
            }

        }

        //[HttpGet("vehiclesingle")]
        //public IActionResult vehicleDetailsBySingle([FromQuery] string UserId, [FromQuery] string VehileId)
        //{
        //    var result = vehicleData.GetVehicleDetailsSingle(UserId,VehileId);
        //    if(!string.IsNullOrEmpty(result.Result?.VehicleId))
        //    {
        //        return Ok(result);
        //    }
        //    else if (result is null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return BadRequest(result);
        //    }
        //}

        [HttpGet("onevehicle")]
        public async Task<IActionResult> OneVehicleData([FromQuery] string vehicleNumber)
        {
            var result = await vehicleData.GetDetailsByVehicleNumber(vehicleNumber);
            if (!string.IsNullOrEmpty(result?.VehicleId))
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
