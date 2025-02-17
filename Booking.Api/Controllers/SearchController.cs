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
        private readonly IManagerFactory _managerFactory;

        public SearchController(IManagerFactory managerFactory)
        {
            _managerFactory = managerFactory;
        }

        [HttpPost]
        public async Task<ActionResult<SearchResponse>> Search([FromBody] SearchRequest request)
        {
            try
            {
                var manager = _managerFactory.CreateManager(request);
                var result = await manager.Search(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
