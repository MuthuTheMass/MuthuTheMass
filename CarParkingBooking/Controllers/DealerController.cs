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
            var result = dealerData.SearchData(filter);

            if (result.Result.Count > 0)
            {
                return Ok(result);
            }
            else if (result.Result.Count == 0)
            {
                return NotFound(result);
            }
            else 
            {
                return BadRequest(result);
            }

        }

        [HttpPost("updatedealer")]
        public IActionResult Add(DealerVM dealerValue) 
        {
            var result = dealerData.UpsertDealerData(dealerValue);

            if (result.Result == true)
            {
                return Ok(result);
            }
            else if(result.Result == null)
            {
                return Conflict("User Doesn't exists.");
            }
            else
            {
                return BadRequest(result);
            }

        }

        [HttpDelete]
        public IActionResult Delete(DeleteDealer deleteDealer) 
        {
            var result = dealerData.RemoveDealer(deleteDealer);

            if (result.Result == true) 
            {
                return Ok(result);
            }
            else if(result.Result == false)
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
