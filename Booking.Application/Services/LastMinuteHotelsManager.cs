using Booking.Application.DTOs.Requests;
using Booking.Application.DTOs.Responses;
using Booking.Application.Interfaces;
using Booking.Application.Interfaces.Repositories;

namespace Booking.Application.Services
{
    public class LastMinuteHotelsManager : HotelOnlyManager
    {
        public LastMinuteHotelsManager(IExternalApiService externalApiService,
            ISearchRepository searchRepository,
            IBookingRepository bookingRepository)
            : base(externalApiService, searchRepository, bookingRepository)
        {
        }

        protected override async Task<SearchResponse> PerformSearch(SearchRequest request)
        {
            var result = await base.PerformSearch(request);

            // Apply last minute discount
            foreach (var option in result.Options)
            {
                option.Price *= 0.8;
            }

            return result;
        }
    }
}
