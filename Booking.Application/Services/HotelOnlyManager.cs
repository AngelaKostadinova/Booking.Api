using Booking.Application.DTOs.Requests;
using Booking.Application.DTOs.Responses;
using Booking.Application.Interfaces;
using Booking.Application.Repositories;
using Booking.Application.Services.WebApi.Services;

namespace Booking.Application.Services
{
    public class HotelOnlyManager : BaseBookingManager
    {
        public HotelOnlyManager(IExternalApiService externalApiService, ISearchRepository searchRepository, IBookingRepository bookingRepository)
            : base(externalApiService, searchRepository, bookingRepository)
        {
        }

        protected override async Task<SearchResponse> PerformSearch(SearchRequest request)
        {
            var hotels = await _externalApiService.GetHotelsAsync(request.Destination);

            return new SearchResponse
            {
                Options = hotels.Select(h => new Option
                {
                    OptionCode = Guid.NewGuid().ToString(),
                    HotelCode = h.HotelCode.ToString(),
                    FlightCode = string.Empty,
                    ArrivalAirport = request.Destination
                }).ToList()
            };
        }
    }
}
