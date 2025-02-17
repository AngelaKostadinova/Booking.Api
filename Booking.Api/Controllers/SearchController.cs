using Booking.Application.DTOs.Requests;
using Booking.Application.DTOs.Responses;
using Booking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IBookingManagerFactory _managerFactory;

        public SearchController(IBookingManagerFactory managerFactory)
        {
            _managerFactory = managerFactory;
        }

        [HttpPost]
        public async Task<ActionResult<SearchResponse>> Search([FromBody] SearchRequest request)
        {
                var manager = _managerFactory.CreateManager(request);
                var result = await manager.Search(request);

                return Ok(result);
        }
    }
}
