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

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] SignUpVM signUp)
        {

            var result = await authorization.ValidateLoginDetials(signUp);

            return Ok(result);


        }
    }
}
