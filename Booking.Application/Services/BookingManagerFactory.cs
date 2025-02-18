
using Booking.Application.DTOs.Requests;
using Booking.Application.Interfaces;
using Booking.Application.Repositories;
using Microsoft.Extensions.Configuration;

namespace Booking.Application.Services
{
    public class BookingManagerFactory : IBookingManagerFactory
    {
        private readonly IExternalApiService _externalApiService;
        private readonly ISearchRepository _searchRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly ISearchRequestValidator _validator;
        private readonly int _lastMinuteDays;

        public BookingManagerFactory(IExternalApiService externalApiService,
            ISearchRepository searchRepository,
            IBookingRepository bookingRepository,
            ISearchRequestValidator validator,
            IConfiguration configuration)
        {
            _externalApiService = externalApiService;
            _searchRepository = searchRepository;
            _bookingRepository = bookingRepository;
            _validator = validator;

            _lastMinuteDays = int.Parse(configuration["ValidationRules:LastMinuteDays"] ??
            throw new ArgumentNullException("Last minute offer days are not present"));
        }

        public IBookingManager CreateManager(SearchRequest request)
        {
            _validator.Validate(request);

            if (string.IsNullOrEmpty(request.DepartureAirport))
            {
                if ((request.FromDate - DateTime.Now).TotalDays <= _lastMinuteDays)
                    return new LastMinuteHotelsManager(_externalApiService, _searchRepository, _bookingRepository);
                return new HotelOnlyManager(_externalApiService, _searchRepository, _bookingRepository);
            }
            return new HotelAndFlightManager(_externalApiService, _searchRepository, _bookingRepository);
        }
    }
}
