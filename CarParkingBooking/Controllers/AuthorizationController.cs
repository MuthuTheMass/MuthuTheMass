using CarParkingBooking.Services_Program;
using CarParkingBookingVM.Enums;
using CarParkingBookingVM.Login;
using CarParkingSystem.Application.Dtos.Authorization;
using CarParkingSystem.Application.Dtos.Dealers;
using CarParkingSystem.Domain.Entities;
using CarParkingSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace CarParkingBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public AuthorizationController(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        [HttpPost("signup")]
        public async Task<ActionResult> SignUp([FromBody] UserInformation signUp)
        {
            
            var result = await userRepository.CreateAsync(signUp);
            // if (result is true)
            // {
            //     return Ok(result);
            // }
            // else if (result is false)
            // {
            //     return UnprocessableEntity(result);
            // }
            // else if(result is null)
            // {
            //     return Conflict(result);
            // }
            // else
            // {
            //     return BadRequest(result);
            // }
            return Ok(result);



        }

        // [HttpPost("userlogin")]
        // public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        // {
        //     var result = await userRepository.VerifyUser(loginDto);
        //     if(result is not null)
        //     {
        //         var token = GenerateJWTToken.GenerateJwtToken(result.UserName, new List<string> { AccessToUser.User });
        //         result.AccessToken = token;
        //         return Ok(result);
        //     }
        //     else if(result is null)
        //     {
        //         return NotFound(result);
        //     }
        //     else
        //     {
        //         return BadRequest(result);
        //     }
        // }
        //
        // [HttpPost("dealersignup")]
        // public async Task<IActionResult> DealerSignUp(DealerSignUpDto dealerSignUp)
        // {
        //     var result = await userRepository.InsertDealerDetails(dealerSignUp);
        //     if(result is true)
        //     {
        //         return Ok(true);
        //     }
        //     else if(result is false)
        //     {
        //         return UnprocessableEntity(result);
        //     }
        //     else if (result is null)
        //     {
        //         return Conflict("This user already available");
        //     }
        //     else
        //     {
        //         return BadRequest(result);
        //     }
        //
        // }
        //
        // [HttpPost("dealerlogin")]
        // public async Task<IActionResult> DealerLogin(DealerLogin dealerLogin)
        // {
        //     var data = await userRepository.VerifyDealer(dealerLogin);
        //     var result = data.Item1;
        //     if (result is not null)
        //     {
        //         var token = GenerateJWTToken.GenerateJwtToken(result.UserName, new List<string> { AccessToUser.Dealer });
        //         result.AccessToken = token;
        //         return Ok(result);
        //     }
        //     else if (!(bool)data.Item2)
        //     {
        //         return Unauthorized("Password doesn't Match.");
        //     }
        //     else if (result is null)
        //     {
        //         return NotFound(result);
        //     }
        //     else
        //     {
        //         return BadRequest(result);
        //     }
        // }
    }
}
