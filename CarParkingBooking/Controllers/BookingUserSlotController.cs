using CarParkingSystem.Application.Dtos.Booking;
using CarParkingSystem.Application.Services.BookingService;
using Microsoft.AspNetCore.Mvc;

namespace CarParkingBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingUserSlotController : ControllerBase
    {
        private readonly IUserBookingService bookingData;

        public BookingUserSlotController(IUserBookingService _bookingData)
        {
            bookingData = _bookingData;
        }


        [HttpPost("Booking")]
        //[Authorize(Policy = AccessToUser.User)]
        public async Task<IActionResult> Booking([FromBody] BookingDto booking)
        {
            var result = await bookingData.AddBooking(booking);
            if (result == true)
            {
                return Ok(result);
            }
            else if (result == false)
            {
                return UnprocessableEntity(result);
            }
            else
            {
                return BadRequest(result);
            }


        }
    }
}
