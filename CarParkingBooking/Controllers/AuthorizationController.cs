using CarParkingBooking.Services_Program;
using CarParkingBookingVM.Authorization;
using CarParkingBookingVM.Enums;
using CarParkingBookingVM.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        public async Task<ActionResult> Login([FromBody] LoginVM loginVM)
        {
            var result = await authorization.VerifyUser(loginVM);
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
        public async Task<IActionResult> DealerSignUp(DealerSignUpVM dealerSignUp)
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
                return Conflict(result);
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

        [HttpPost("dealerlogout")]
        public IActionResult DealerLogout()
        {
            return Ok();
        }
    }
}
