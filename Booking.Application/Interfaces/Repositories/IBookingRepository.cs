using Booking.Domain.Enums;

namespace Booking.Application.Interfaces.Repositories
{
    public interface IBookingRepository
    {
        Task StoreBookingAsync(BookingInfo booking);
        Task<BookingInfo> GetBookingAsync(string bookingCode);
        Task UpdateBookingStatusAsync(string bookingCode, BookingStatusEnum status);
    }
}