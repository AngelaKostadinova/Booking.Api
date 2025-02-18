using Booking.Application.DTOs.Requests;

namespace Booking.Application.Interfaces
{
    public interface ISearchRequestValidator
    {
        void Validate(SearchRequest request);
    }
}