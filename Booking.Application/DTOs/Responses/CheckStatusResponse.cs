using Booking.Domain.Enums;

namespace Booking.Application.DTOs.Responses
{
    public class CheckStatusResponse
    {
        public BookingStatusEnum Status { get; set; }
    }
}
