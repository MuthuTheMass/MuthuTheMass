using System.Diagnostics.CodeAnalysis;
using CarParkingBooking.QRCodeGenerator.PDFGenerator;
using CarParkingSystem.Application.Dtos.Booking;
using CarParkingSystem.Application.Services.BookingService;
using CarParkingSystem.Domain.Dtos.Booking.Payment;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace CarParkingBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingUserSlotController(IUserBookingService bookingData, IGeneratePdf generatePdf) : ControllerBase
    {
        [HttpPost("Booking")]
        //[Authorize(Policy = AccessToUser.Dealer)]
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

        [HttpGet]
        [Route(nameof(GetSingleBookingDetailByBookingId))]
        public async Task<IActionResult> GetSingleBookingDetailByBookingId([FromQuery] string bookingId)
        {
            var result = await bookingData.GetSingleBookingDetailByBookingIdAsync(bookingId);
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
            var result = await bookingData.GetSingleBookingAsync(EncryptedId);
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
            var confirmedBooking = await bookingData.GetSingleBookingDetailByBookingIdAsync(id);
            var pdfBytes = await generatePdf.BookingConfirmation(confirmedBooking);
            return File(pdfBytes, "application/pdf", $"ZenPark_{confirmedBooking.BookingId}");
        }


        #region UserSide

        [HttpPost("UserBooking")]
        public async Task<IActionResult> UserBooking([FromBody] BookingDto booking)
        {
            var result = await bookingData.AddBooking(booking);
            if (result == true)
            {
                return Ok(result);
            }
            if (result == false)
            {
                return UnprocessableEntity(result);
            }
            return BadRequest(result);
        }

        [HttpGet("UserBooking")]
        public async Task<IActionResult> GetBookingDetailByBookingId([FromQuery] DateTime dateTime,
            [FromQuery] string customerEmail)
        {
            var result = await bookingData.GetSingleBookingAsync(dateTime, customerEmail);
            return Ok(result);
        }

        [HttpGet]
        [Route(nameof(GetBookingHistoryForUser))]
        public async Task<IActionResult> GetBookingHistoryForUser([FromQuery] string emailId)
        {
            var result = await bookingData.GetUserBookingHistoryAsync(emailId);
            if (result.Count() >= 0)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet]
        [Route(nameof(FetchUserBookingDownload))]
        public async Task<IActionResult> FetchUserBookingDownload([FromQuery] string bookingId)
        {
            var result = await bookingData.GetFirstBookingDetailByBookingIdAsync(bookingId);
            return Ok(result ?? null);

        }

        [HttpPost]
        [Route(nameof(ProcessUserBookingPayment))]
        public async Task<IActionResult> ProcessUserBookingPayment([FromBody] UserPayment userPayment)
        {
            var result = await bookingData.ProcessPaymentForUserBooking(userPayment);
            if (result is not null)
                return Ok(result);
            return result == false ? UnprocessableEntity(result) : BadRequest(result);

        }

        [HttpGet]
        [Route(nameof(DownloadUserPdf))]
        public async Task<IActionResult> DownloadUserPdf(string bookingId)
        {
            var result = await bookingData.GenerateUserPDF(bookingId);
            return File(result, "application / pdf", "BookingConfirmation.pdf");
        }

        #endregion
    }
}