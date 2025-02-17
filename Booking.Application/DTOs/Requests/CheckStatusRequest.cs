using System.ComponentModel.DataAnnotations;

namespace Booking.Application.DTOs.Requests
{
    public class CheckStatusRequest
    {
        [Required]
        public string BookingCode { get; set; }
    }
}
