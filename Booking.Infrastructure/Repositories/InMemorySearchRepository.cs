using Booking.Application.DTOs.Responses;
using Booking.Application.Exceptions;
using Booking.Application.Interfaces.Repositories;

namespace Booking.Application.Repositories
{
    public class InMemorySearchRepository : ISearchRepository
    {
        private readonly Dictionary<string, SearchResponse> _searchResults = new();
        private readonly Dictionary<string, Option> _optionsByCode = new();
        private readonly Dictionary<string, DateTime> _searchTimes = new();

        public async Task StoreSearchResultsAsync(string searchId, SearchResponse searchResponse)
        {
            _searchResults[searchId] = searchResponse;
            _searchTimes[searchId] = DateTime.UtcNow;

            foreach (var option in searchResponse.Options)
            {
                _optionsByCode[option.OptionCode] = option;
            }
            await Task.CompletedTask;
        }

        public async Task<Option> GetOptionByCodeAsync(string optionCode)
        {
            if (!_optionsByCode.TryGetValue(optionCode, out var option))
            {
                throw new NotFoundException("Option not found");
            }
            return await Task.FromResult(option);
        }

        public async Task ClearOldSearchesAsync(TimeSpan threshold)
        {
            var now = DateTime.UtcNow;
            var expiredSearchIds = _searchTimes
                .Where(kvp => now - kvp.Value > threshold)
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var searchId in expiredSearchIds)
            {
                if (_searchResults.TryGetValue(searchId, out var searchResponse))
                {
                    foreach (var option in searchResponse.Options)
                    {
                        _optionsByCode.Remove(option.OptionCode);
                    }
                }
                _searchResults.Remove(searchId);
                _searchTimes.Remove(searchId);
            }
            await Task.CompletedTask;
        }
    }
}