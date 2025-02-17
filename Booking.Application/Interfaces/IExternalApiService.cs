using Booking.Application.DTOs.Responses;

namespace Booking.Application.Interfaces
{
    public interface IExternalApiService
    {
        Task<IEnumerable<HotelResponse>> GetHotelsAsync(string destinationCode);
        Task<IEnumerable<FlightResponse>> GetFlightsAsync(string departureAirport, string arrivalAirport);
    }
}
