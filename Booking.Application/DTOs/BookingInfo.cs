using Booking.Application.DTOs.Responses;
using Booking.Domain.Enums;

public class BookingInfo
{
    public string BookingCode { get; set; }
    public DateTime BookingTime { get; set; }
    public int SleepTime { get; set; }
    public BookingStatusEnum Status { get; set; }
    public string SearchType { get; set; }
    public Option Option { get; set; }
}