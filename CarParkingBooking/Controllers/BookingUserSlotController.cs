using CarParkingBookingVM.Enums;
using CarParkingSystem.Application.Dtos.Booking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidateCarParkingDetails.ValidateAuthorization;

namespace CarParkingBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingUserSlotController : ControllerBase
    {
        private readonly IBookingData bookingData;

        public BookingUserSlotController(IBookingData _bookingData)
        {
            bookingData = _bookingData;
        }


        [HttpPost("Booking")]
        //[Authorize(Policy = AccessToUser.User)]
        public async Task<IActionResult> Booking([FromBody]BookingDto booking)
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
