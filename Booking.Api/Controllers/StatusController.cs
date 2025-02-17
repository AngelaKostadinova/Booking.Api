using Booking.Application.DTOs.Requests;
using Booking.Application.DTOs.Responses;
using Booking.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly HotelOnlyManager _defaultManager;

        public StatusController(HotelOnlyManager defaultManager)
        {
            _defaultManager = defaultManager;
        }

        [HttpPost] // should this be get?
        public async Task<ActionResult<CheckStatusResponse>> CheckStatus([FromBody] CheckStatusRequest request)
        {
            try
            {
                var result = await _defaultManager.CheckStatus(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
