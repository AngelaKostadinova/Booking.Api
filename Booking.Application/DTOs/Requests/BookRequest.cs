using System.ComponentModel.DataAnnotations;

namespace Booking.Application.DTOs.Requests
{
    public class BookRequest
    {
        [Required]
        public string OptionCode { get; set; }

        [Required]
        public SearchRequest SearchReq { get; set; }
    }
}
