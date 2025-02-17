using Booking.Application.DTOs.Requests;
using Booking.Application.DTOs.Responses;
using Booking.Application.Interfaces;
using Booking.Application.Services.WebApi.Services;

namespace Booking.Application.Services
{
    public class HotelOnlyManager : BaseBookingManager
    {
        public HotelOnlyManager(IExternalApiService externalApiService) : base(externalApiService)
        {
        }

        public override async Task<SearchResponse> Search(SearchRequest request)
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
