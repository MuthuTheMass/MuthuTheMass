using CarParkingBooking.Services_Program;
using CarParkingBookingVM.Enums;
using CarParkingBookingVM.Login;
using CarParkingSystem.Application.Dtos.Authorization;
using CarParkingSystem.Application.Dtos.Dealers;
using CarParkingSystem.Application.Services.ValidateAuthorization;
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
        public async Task<ActionResult> SignUp([FromBody] SignUpDto signUp)
        {

            var result = await authorization.InsertLoginDetials(signUp);
            if (result is true)
            {
                return Ok(result);
            }
            else if (result is false)
            {
                return UnprocessableEntity(result);
            }
            else if(result is null)
            {
                return Conflict(result);
            }
            else
            {
                return BadRequest(result);
            }



        }

        [HttpPost("userlogin")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await authorization.VerifyUser(loginDto);
            if(result is not null)
            {
                var token = GenerateJWTToken.GenerateJwtToken(result.UserName, new List<string> { AccessToUser.User });
                result.AccessToken = token;
                return Ok(result);
            }
            else if(result is null)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("dealersignup")]
        public async Task<IActionResult> DealerSignUp(DealerSignUpDto dealerSignUp)
        {
            var result = await authorization.InsertDealerDetails(dealerSignUp);
            if(result is true)
            {
                return Ok(true);
            }
            else if(result is false)
            {
                return UnprocessableEntity(result);
            }
            else if (result is null)
            {
                return Conflict("This user already available");
            }
            else
            {
                return BadRequest(result);
            }

        }

        [HttpPost("dealerlogin")]
        public async Task<IActionResult> DealerLogin(DealerLogin dealerLogin)
        {
            var data = await authorization.VerifyDealer(dealerLogin);
            var result = data.Item1;
            if (result is not null)
            {
                var token = GenerateJWTToken.GenerateJwtToken(result.UserName, new List<string> { AccessToUser.Dealer });
                result.AccessToken = token;
                return Ok(result);
            }
            else if (!(bool)data.Item2)
            {
                return Unauthorized("Password doesn't Match.");
            }
            else if (result is null)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
