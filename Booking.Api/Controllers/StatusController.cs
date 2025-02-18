using Booking.Api.Authentication;
using Booking.Application.DTOs.Requests;
using Booking.Application.DTOs.Responses;
using Booking.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [ApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly HotelOnlyManager _defaultManager;

        public StatusController(HotelOnlyManager defaultManager)
        {
            _defaultManager = defaultManager;
        }

        [HttpGet("{bookingCode}")]
        public async Task<ActionResult<CheckStatusResponse>> CheckStatus(string bookingCode)
        {
            var request = new CheckStatusRequest { BookingCode = bookingCode };
            var result = await _defaultManager.CheckStatus(request);
            return Ok(result);
        }
    }
}
