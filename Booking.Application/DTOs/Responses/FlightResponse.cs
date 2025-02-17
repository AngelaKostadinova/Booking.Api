namespace Booking.Application.DTOs.Responses
{
    public class FlightResponse
    {
        public int FlightCode { get; set; }

        public string FlightNumber { get; set; }

        public string DepartureAirport { get; set; }

        public string ArrivalAirport { get; set; }
    }
}
