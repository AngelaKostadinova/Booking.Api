using Booking.Application.DTOs.Requests;
using Booking.Application.DTOs.Responses;
using Booking.Application.Interfaces;

namespace Booking.Application.Services
{
    public class LastMinuteHotelsManager : HotelOnlyManager
    {
        public LastMinuteHotelsManager(IExternalApiService externalApiService) : base(externalApiService)
        {
        }

        public override async Task<SearchResponse> Search(SearchRequest request)
        {
            var result = await base.Search(request);

            // Apply last minute discount
            foreach (var option in result.Options)
            {
                option.Price *= 0.8; // 20% discount
            }

            return result;
        }
    }
}
