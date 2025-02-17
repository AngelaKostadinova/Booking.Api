using System.Text.Json.Serialization;

namespace Booking.Application.DTOs.Responses
{
    public class HotelResponse // make record
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("hotelCode")]
        public int HotelCode { get; set; }

        [JsonPropertyName("hotelName")]
        public string HotelName { get; set; }

        [JsonPropertyName("destinationCode")]
        public string DestinationCode { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }
    }
}
