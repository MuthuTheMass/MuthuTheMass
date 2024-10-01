using CarParkingBookingVM.Authorization;
using CarParkingBookingVM.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidateCarParkingDetails.ValidateAuthorization;

namespace CarParkingBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorization authorization;

        public AuthorizationController(IAuthorization _authorization)
        {
            authorization = _authorization;
        }

        [HttpPost("signup")]
        public async Task<ActionResult> SignUp([FromBody] SignUpVM signUp)
        {

            var result = await authorization.UpsertLoginDetials(signUp);
            if (result is true)
            {
                return Ok(result);
            }
            else if (result is false)
            {
                return UnprocessableEntity(result);
            }
            else
            {
                return BadRequest(result);
            }



        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginVM loginVM)
        {
            var result = await authorization.VerifyUser(loginVM);
            if(result is true)
            {
                return Ok(result);
            }
            else if(result is false)
            {

            }
        }

        //[HttpPost("alreadyexists")]
        //public async Task<ActionResult> AlreadyExists()
        //{

        //}
    }
}
