using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarParkingBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerController : ControllerBase
    {

        [HttpPost]
        public IActionResult Search()
        {

        }
    }
}
