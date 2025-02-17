using System.ComponentModel.DataAnnotations;

namespace Booking.Application.DTOs.Requests
{
    public class SearchRequest
    {
        [Required]
        public string Destination { get; set; }
        public string? DepartureAirport { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
    }
}
