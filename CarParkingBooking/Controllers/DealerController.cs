using CarParkingBookingVM.VM_S.Dealers;
using Microsoft.AspNetCore.Mvc;
using ValidateCarParkingDetails.ValidateAuthorization;

namespace CarParkingBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerController : ControllerBase
    {
        private readonly IDealerData dealerData;

        public DealerController(IDealerData _dealerData)
        {
            dealerData = _dealerData;
        }


        [HttpPost("search")]
        public IActionResult Search(Filter filter)
        {
            return Ok(dealerData.SearchData(filter));
        }
    }
}
