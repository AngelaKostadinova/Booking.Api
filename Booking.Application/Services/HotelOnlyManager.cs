using Booking.Application.DTOs.Requests;
using Booking.Application.DTOs.Responses;
using Booking.Application.Exceptions;
using Booking.Application.Interfaces;
using Booking.Application.Repositories;
using Booking.Application.Services.WebApi.Services;
using Booking.Application.Utils;
using System.Security.Cryptography;

namespace Booking.Application.Services
{
    public class HotelOnlyManager : BaseBookingManager
    {
        public HotelOnlyManager(IExternalApiService externalApiService,
            ISearchRepository searchRepository,
            IBookingRepository bookingRepository)
            : base(externalApiService, searchRepository, bookingRepository)
        {
        }

        protected override async Task<SearchResponse> PerformSearch(SearchRequest request)
        {
            var hotels = await _externalApiService.GetHotelsAsync(request.Destination);

            if (!hotels.Any())
            {
                throw new NotFoundException($"No hotels found for destination: {request.Destination}");
            }

            return new SearchResponse
            {
                Options = hotels.Select(h => new Option
                {
                    OptionCode = CodeGenerator.GenerateCode(),
                    HotelCode = h.HotelCode.ToString(),
                    FlightCode = string.Empty,
                    ArrivalAirport = request.Destination,
                    Price = RandomNumberGenerator.GetInt32(100, 1000)
                }).ToList()
            };
        }
    }
}
