using CarParkingSystem.Application.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace CarParkingBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserProfile _userData;

        public UsersController(IUserProfile userData)
        {
            _userData = userData;
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


        //[HttpPost("updateuser")]
        //public async Task<IActionResult> UpdateuserDetails([FromForm] UserDataDto details)
        //{
        //    var result = await _userData.UpdateUserData(details);
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    else if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    else if (result == false)
        //    {
        //        return UnprocessableEntity();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpGet("getAllUsers")]
        //public async Task<IActionResult> GetAllUsers()
        //{
        //    var result = await _userData.GetAllUsers();

        //    if (result.Count > 0)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest();

        //}
    }
}