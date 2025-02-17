using Booking.Domain.Enums;
using System.Threading.Tasks;

namespace Booking.Application.Repositories
{
    public interface IBookingRepository
    {
        Task StoreBookingAsync(BookingInfo booking);
        Task<BookingInfo> GetBookingAsync(string bookingCode);
        Task UpdateBookingStatusAsync(string bookingCode, BookingStatusEnum status);
    }
} 