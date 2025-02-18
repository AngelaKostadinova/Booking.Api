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
    public class HotelAndFlightManager : BaseBookingManager
    {
        public HotelAndFlightManager(IExternalApiService externalApiService,
            ISearchRepository searchRepository,
            IBookingRepository bookingRepository)
            : base(externalApiService, searchRepository, bookingRepository)
        {
        }

        protected override async Task<SearchResponse> PerformSearch(SearchRequest request)
        {
            var hotelsTask = _externalApiService.GetHotelsAsync(request.Destination);
            var flightsTask = _externalApiService.GetFlightsAsync(
                request.DepartureAirport,
                request.Destination);

            await Task.WhenAll(hotelsTask, flightsTask);

            var hotels = await hotelsTask;
            var flights = await flightsTask;

            if (!hotels.Any())
            {
                throw new NotFoundException($"No hotels found for destination: {request.Destination}");
            }

            if (!flights.Any())
            {
                throw new NotFoundException(
                    $"No flights found from {request.DepartureAirport} to {request.Destination}");
            }

            return new SearchResponse
            {
                Options = hotels.SelectMany(h => flights, (h, f) => new Option
                {
                    OptionCode = CodeGenerator.GenerateCode(),
                    HotelCode = h.HotelCode.ToString(),
                    FlightCode = f.FlightCode.ToString(),
                    ArrivalAirport = request.Destination,
                    Price = RandomNumberGenerator.GetInt32(100, 1000)
                }).ToList()
            };
        }
    }
}
