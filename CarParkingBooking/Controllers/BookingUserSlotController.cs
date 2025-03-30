using CarParkingSystem.Application.Dtos.Booking;
using CarParkingSystem.Application.Services.BookingService;
using Microsoft.AspNetCore.Mvc;

namespace CarParkingBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingUserSlotController : ControllerBase
    {
        private readonly IUserBookingService _bookingData;

        public BookingUserSlotController(IUserBookingService bookingData)
        {
            _bookingData = bookingData;
        }


        [HttpPost("Booking")]
        //[Authorize(Policy = AccessToUser.User)]
        public async Task<IActionResult> Booking([FromBody] BookingDto booking)
        {
            var result = await _bookingData.AddBooking(booking);
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

        [HttpGet]
        [Route(nameof(GetSingleBookingDetailByBookingId))]
        public async Task<IActionResult> GetSingleBookingDetailByBookingId([FromQuery] string bookingId)
        {
            var result = await _bookingData.GetSingleBookingDetialByBookingIdAsync(bookingId);
            if (result?.BookingId is not null)
            {
                return Ok(result);
            }
            else if (result?.BookingId is null)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet]
        [Route(nameof(GetBookingDetailByEncryptedId))]
        public async Task<IActionResult> GetBookingDetailByEncryptedId(string EncryptedId)
        {
            var result = await _bookingData.GetSingleBookingAsync(EncryptedId);
            if (result?.BookingId is not null)
            {
                return Ok(result);
            }
            else if (result?.BookingId is null)
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
