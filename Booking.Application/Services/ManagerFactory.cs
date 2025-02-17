
using Booking.Application.DTOs.Requests;
using Booking.Application.Interfaces;

namespace Booking.Application.Services
{
    public class ManagerFactory : IManagerFactory
    {
        private readonly IExternalApiService _externalApiService;

        public ManagerFactory(IExternalApiService externalApiService)
        {
            _externalApiService = externalApiService;
        }

        public IBookingManager CreateManager(SearchRequest request)
        {
            if (string.IsNullOrEmpty(request.DepartureAirport))
            {
                if ((request.FromDate - DateTime.Now).TotalDays <= 45)
                    return new LastMinuteHotelsManager(_externalApiService);
                return new HotelOnlyManager(_externalApiService);
            }
            return new HotelAndFlightManager(_externalApiService);
        }
    }
}
