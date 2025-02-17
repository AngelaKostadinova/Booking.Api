using Booking.Application.DTOs.Requests;

namespace Booking.Application.Interfaces
{
    public interface IManagerFactory
    {
        IBookingManager CreateManager(SearchRequest request);
    }
}
