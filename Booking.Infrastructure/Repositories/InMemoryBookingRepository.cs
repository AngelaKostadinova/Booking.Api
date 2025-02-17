using Booking.Application.Exceptions;
using Booking.Domain.Enums;

namespace Booking.Application.Repositories
{
    public class InMemoryBookingRepository : IBookingRepository
    {
        private readonly Dictionary<string, BookingInfo> _bookings = new();

        public async Task StoreBookingAsync(BookingInfo booking)
        {
            _bookings[booking.BookingCode] = booking;
            await Task.CompletedTask;
        }

        public async Task<BookingInfo> GetBookingAsync(string bookingCode)
        {
            if (!_bookings.TryGetValue(bookingCode, out var booking))
            {
                throw new NotFoundException("Booking not found");
            }
            return await Task.FromResult(booking);
        }

        public async Task UpdateBookingStatusAsync(string bookingCode, BookingStatusEnum status)
        {
            if (_bookings.TryGetValue(bookingCode, out var booking))
            {
                booking.Status = status;
            }
            await Task.CompletedTask;
        }
    }
}