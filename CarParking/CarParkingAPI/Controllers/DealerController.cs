using CarParkingSystem.Application.Dtos.Booking;
using CarParkingSystem.Application.Services.DealerService;
using CarParkingSystem.Domain.Dtos.Dealers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CarParkingBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerController : ControllerBase
    {
        private readonly IDealerProfile dealerData;

        public DealerController(IDealerProfile _dealerData)
        {
            dealerData = _dealerData;
        }


        [HttpPost("search")]
        public async Task<IActionResult> Search(Filter filter)
        {
            var result = await dealerData.GetAllDealersBySearch(filter);

            if (result.Data.Count > 0)
            {
                return Ok(result);
            }
            else if (result.Data.Count == 0)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("dealernewusers")]
        public async Task<IActionResult> dealerDashboard([FromQuery] string emailId)
        {
            var result = await dealerData.GetUsersByDealer(emailId);

            return Ok(result);
        }

        [HttpPost("OfflineBooking")]
        public async Task<IActionResult> OfflineBooking(BookingDto offlineBooking)
        {
            var result = await dealerData.DealerBookingOffline(offlineBooking);
            if (result == true)
            {
                return Ok(result);
            }
            else if (result == false)
            {
                return BadRequest(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpGet("DealerBookings")]
        public async Task<IActionResult> GetAllBookingDetails([FromQuery] string emailId)
        {
            var result = await dealerData.GetAllBookingsByDealerEmailId(emailId);
            if (result.Count >= 0)
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

        [HttpGet]
        [Route(nameof(AdvanceAmountOfDealer))]
        public async Task<IActionResult> AdvanceAmountOfDealer(string dealerEmail)
        {
            var result = await dealerData.GetDealerByEmail(dealerEmail);
            return Ok(result.OneHourAmount);
        }
    }
}