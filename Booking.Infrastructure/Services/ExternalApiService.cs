using Booking.Application.DTOs.Responses;
using Booking.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json;

namespace Booking.Infrastructure.Services
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ExternalApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ExternalApi:BaseUrl"];
        }

        public async Task<IEnumerable<HotelResponse>> GetHotelsAsync(string destinationCode)
        {
            var response = await _httpClient.GetAsync(
                $"{_baseUrl}/SearchHotels?destinationCode={destinationCode}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var hotels = JsonSerializer.Deserialize<IEnumerable<HotelResponse>>(content);
            return hotels ?? Enumerable.Empty<HotelResponse>();
        }

        public async Task<IEnumerable<FlightResponse>> GetFlightsAsync(
            string departureAirport, string arrivalAirport)
        {
            var response = await _httpClient.GetAsync(
                $"{_baseUrl}/SearchFlights?departureAirport={departureAirport}&arrivalAirport={arrivalAirport}");
            response.EnsureSuccessStatusCode();

            var flights = await response.Content.ReadFromJsonAsync<IEnumerable<FlightResponse>>();
            return flights ?? Enumerable.Empty<FlightResponse>();
        }
    }
}
