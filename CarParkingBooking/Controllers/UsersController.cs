using CarParkingBookingVM.VM_S;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidateCarParkingDetails.ValidateAuthorization;

namespace CarParkingBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserData userData;

        public UsersController(IUserData _userData)
        {
            userData = _userData;
        }

        [HttpGet("userfull")]
        public async Task<IActionResult> Get(string userEmail)
        {
            var result = await userData.GetSingleUser(userEmail);

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


        [HttpPost("updateuser")]
        public async Task<IActionResult> UpdateuserDetailsAsync([FromForm]UserData details)
        {
            var result = await userData.UpdateUserData(details);
            if(result != null)
            {
                return Ok(result);
            }
            else if(result == null)
            {
                return NotFound();
            }
            else if(result == false)
            {
                return UnprocessableEntity();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
