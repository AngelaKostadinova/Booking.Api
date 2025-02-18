using Booking.Application.DTOs.Requests;

namespace Booking.Application.Interfaces.Repositories
{
    public interface ISearchRequestValidator
    {
        void Validate(SearchRequest request);
    }
}