
using Booking.Application.DTOs.Requests;
using Booking.Application.Interfaces;
using Booking.Application.Repositories;

namespace Booking.Application.Services
{
    public class BookingManagerFactory : IBookingManagerFactory
    {
        private readonly IExternalApiService _externalApiService;
        private readonly ISearchRepository _searchRepository;
        private readonly IBookingRepository _bookingRepository;

        public BookingManagerFactory(IExternalApiService externalApiService, ISearchRepository searchRepository, IBookingRepository bookingRepository)
        {
            _externalApiService = externalApiService;
            _searchRepository = searchRepository;
            _bookingRepository = bookingRepository;
        }

        public IBookingManager CreateManager(SearchRequest request)
        {
            if (string.IsNullOrEmpty(request.DepartureAirport))
            {
                if ((request.FromDate - DateTime.Now).TotalDays <= 45)
                    return new LastMinuteHotelsManager(_externalApiService, _searchRepository, _bookingRepository);
                return new HotelOnlyManager(_externalApiService, _searchRepository, _bookingRepository);
            }
            return new HotelAndFlightManager(_externalApiService, _searchRepository, _bookingRepository);
        }
    }
}
