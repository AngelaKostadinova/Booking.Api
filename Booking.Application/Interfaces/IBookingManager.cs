using Booking.Application.DTOs.Requests;
using Booking.Application.DTOs.Responses;

namespace Booking.Application.Interfaces
{
    public interface IBookingManager
    {
        Task<SearchResponse> Search(SearchRequest request);
        Task<BookResponse> Book(BookRequest request);
        Task<CheckStatusResponse> CheckStatus(CheckStatusRequest request);
    }
}
