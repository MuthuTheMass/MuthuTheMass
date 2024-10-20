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

        [HttpPost("updateuser")]
        public async Task<IActionResult> UpdateuserDetailsAsync(UserUpdateDetails details)
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
