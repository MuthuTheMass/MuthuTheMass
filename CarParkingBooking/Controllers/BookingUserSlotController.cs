using CarParkingBookingVM.VM_S.Booking;
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
        public IActionResult Booking([FromForm]BookingVM booking)
        {
            return Ok(bookingData.AddBooking(booking));
        }
    }
}
