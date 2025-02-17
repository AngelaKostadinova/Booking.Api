using Booking.Application.DTOs.Requests;

namespace Booking.Application.Interfaces
{
    public interface IBookingManagerFactory
    {
        IBookingManager CreateManager(SearchRequest request);
    }
}
