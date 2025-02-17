using Booking.Application.DTOs.Requests;
using Booking.Application.DTOs.Responses;
using Booking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookingManagerFactory _managerFactory;

        public BookController(IBookingManagerFactory managerFactory)
        {
            _managerFactory = managerFactory;
        }

        [HttpPost]
        public async Task<ActionResult<BookResponse>> Book([FromBody] BookRequest request)
        {
                var manager = _managerFactory.CreateManager(request.SearchReq);
                var result = await manager.Book(request);

                return Ok(result);
        }
    }
}
