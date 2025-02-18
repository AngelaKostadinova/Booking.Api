using Booking.Application.DTOs.Responses;

namespace Booking.Application.Interfaces.Repositories
{
    public interface ISearchRepository
    {
        Task StoreSearchResultsAsync(string searchId, SearchResponse searchResponse);
        Task<Option> GetOptionByCodeAsync(string optionCode);
        Task ClearOldSearchesAsync(TimeSpan threshold);
    }
}