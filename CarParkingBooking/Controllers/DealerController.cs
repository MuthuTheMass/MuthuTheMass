using CarParkingSystem.Application.Dtos.Dealers;
using CarParkingSystem.Application.Services.DealerService;
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

            if (result.Count > 0)
            {
                return Ok(result);
            }
            else if (result.Count == 0)
            {
                return NotFound(result);
            }
            else 
            {
                return BadRequest(result);
            }

        }

        [HttpGet("dealernewusers")]
        public async Task<IActionResult> GetAllUserByDealer([FromQuery]string userName)
        {
            var result = await dealerData.GetUsersByDealer(userName);

            if (result.Count > 0)
            {
                return Ok(result);
            }
            else if (result.Count == 0)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // [HttpPost("singledealerdata")]
        // public async Task<IActionResult> SingleDealerData([FromQuery] string email)
        // {
        //     var result = await dealerData.SingleDealerDetails(email);
        //     
        //     if (!string.IsNullOrEmpty(result.DealerEmail))
        //     {
        //         return Ok(result);
        //     }
        //     return BadRequest(result);
        // }
        //
        // [HttpPost("updatedealer")]
        // public IActionResult AddOrUpdate(DealerDto dealerValue) 
        // {
        //     var result = dealerData.UpsertDealerData(dealerValue);
        //
        //     if (result.Result == true)
        //     {
        //         return Ok(result);
        //     }
        //     else if(result.Result == null)
        //     {
        //         return Conflict("User Doesn't exists.");
        //     }
        //     else
        //     {
        //         return BadRequest(result);
        //     }
        //
        // }
        //
        // [HttpDelete]
        // public IActionResult Delete(DeleteDealer deleteDealer) 
        // {
        //     var result = dealerData.RemoveDealer(deleteDealer);
        //
        //     if (result.Result == true) 
        //     {
        //         return Ok(result);
        //     }
        //     else if(result.Result == false)
        //     {
        //         return NotFound(result);
        //     }
        //     else
        //     {
        //         return BadRequest(result);
        //     }
        // }
        
    }
}
