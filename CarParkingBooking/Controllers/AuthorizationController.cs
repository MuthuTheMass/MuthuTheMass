using CarParkingBookingVM.Enums;
using CarParkingBookingVM.Login;
using CarParkingSystem.Application.Dtos.Authorization;
using CarParkingSystem.Application.Services.Authorization;
using CarParkingSystem.Application.Services.DealerService;
using CarParkingSystem.Application.Services.UserService;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;

namespace CarParkingBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserProfile _userProfile;
        private readonly IDealerProfile _dealerProfile;

        public AuthorizationController(IAuthorizationService authorizationService, IUserProfile userProfile,
            IDealerProfile dealerProfile)
        {
            _authorizationService = authorizationService;
            _userProfile = userProfile;
            _dealerProfile = dealerProfile;
        }

        [HttpPost("usersignup")]
        public async Task<ActionResult> SignUp([FromBody] SignUpDto signUp)
        {
            var result = await _userProfile.UserSignUp(signUp);
            if (result is true)
            {
                return Ok(result);
            }
            else if (result is false)
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

        [HttpPost("userlogin")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authorizationService.UserIsAuthorized(loginDto);
            if (result is not null)
            {
                return Ok(result);
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

        [HttpPost("dealersignup")]
        public async Task<IActionResult> DealerSignUp(SignUpDto dealerSignUp)
        {
            var result = await _dealerProfile.DealerSignUp(dealerSignUp);
            if (result is true)
            {
                return Ok(true);
            }
            else if (result is false)
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
        public async Task<IActionResult> DealerLogin(LoginDto dealerLogin)
        {
            var result = await _authorizationService.DealerIsAuthorized(dealerLogin);
            if (result is not null)
            {
                return Ok(result);
            }
            else if (result?.Access is AccessToUser.Dealer)
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