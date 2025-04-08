using CarParkingBooking.QRCodeGenerator.PDFGenerator;
using CarParkingSystem.Application.Dtos.Booking;
using CarParkingSystem.Application.Services.BookingService;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;

namespace CarParkingBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingUserSlotController : ControllerBase
    {
        private readonly IUserBookingService _bookingData;
        private readonly IGeneratePdf _generatePdf;

        public BookingUserSlotController(IUserBookingService bookingData, IGeneratePdf generatePdf)
        {
            _bookingData = bookingData;
            _generatePdf = generatePdf;
        }


        [HttpPost("Booking")]
        //[Authorize(Policy = AccessToUser.Dealer)]
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

        [HttpGet("generate-pdf/{id}")]
        public async Task<IActionResult> GenerateBookingPdf(string id)
        {
            var confirmedBooking = await _bookingData.GetSingleBookingDetialByBookingIdAsync(id);
            var pdfBytes = await _generatePdf.BookingConfirmation(confirmedBooking);
            return File(pdfBytes, "application/pdf", $"ZenPark_{confirmedBooking.BookingId}");
        }
        
        
        #region UserSide

        [HttpPost("UserBooking")]
        public async Task<IActionResult> UserBooking([FromBody] BookingDto booking)
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

        [HttpGet("UserBooking")]
        public async Task<IActionResult> GetBookingDetailByBookingId([FromQuery] DateTime dateTime,
            [FromQuery] string customerEmail)
        {
            var result = await _bookingData.GetSingleBookingAsync(dateTime, customerEmail);
            return Ok(result);
        }
        
        #endregion
    }
}