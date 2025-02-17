using Booking.Application.DTOs.Responses;
using System.Threading.Tasks;

namespace Booking.Application.Repositories
{
    public interface ISearchRepository
    {
        Task StoreSearchResultsAsync(string searchId, SearchResponse searchResponse);
        Task<Option> GetOptionByCodeAsync(string optionCode);
        Task ClearOldSearchesAsync(TimeSpan threshold);
    }
} 