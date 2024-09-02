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

            return Ok(result);


        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginVM loginVM)
        {
            var result = await authorization.VerifyUser(loginVM);
            return Ok(result);
        }
    }
}
