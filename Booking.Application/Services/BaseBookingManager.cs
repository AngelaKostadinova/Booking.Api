using Booking.Application.DTOs.Requests;
using Booking.Application.DTOs.Responses;
using Booking.Application.Interfaces;
using Booking.Domain.Enums;

namespace Booking.Application.Services
{
    namespace WebApi.Services
    {
        public abstract class BaseBookingManager : IBookingManager
        {
            protected static readonly Dictionary<string, BookingInfo> _bookings = new();

            protected readonly IExternalApiService _externalApiService;

            protected BaseBookingManager(IExternalApiService externalApiService)
            {
                _externalApiService = externalApiService;
            }

            protected class BookingInfo
            {
                public string BookingCode { get; set; }
                public DateTime BookingTime { get; set; }
                public int SleepTime { get; set; }
                public BookingStatusEnum Status { get; set; }
                public string SearchType { get; set; }
            }

            //this can be protected?
            public abstract Task<SearchResponse> Search(SearchRequest request);

            //why is this async there is no await used
            public async Task<BookResponse> Book(BookRequest request)
            {
                var bookingCode = GenerateBookingCode();
                var sleepTime = Random.Shared.Next(30, 61);

                _bookings[bookingCode] = new BookingInfo
                {
                    BookingCode = bookingCode,
                    BookingTime = DateTime.Now,
                    SleepTime = sleepTime,
                    Status = BookingStatusEnum.Pending,
                    SearchType = GetType().Name
                };

                return new BookResponse
                {
                    BookingCode = bookingCode,
                    BookingTime = DateTime.Now
                };
            }

            public async Task<CheckStatusResponse> CheckStatus(CheckStatusRequest request)
            {
                if (!_bookings.TryGetValue(request.BookingCode, out var booking))
                {
                    // should I throw an exception here?
                    throw new KeyNotFoundException("Booking not found");
                }

                var elapsedTime = (DateTime.Now - booking.BookingTime).TotalSeconds;

                if (elapsedTime < booking.SleepTime)
                {
                    return new CheckStatusResponse { Status = BookingStatusEnum.Pending };
                }

                booking.Status = booking.SearchType == nameof(LastMinuteHotelsManager)
                    ? BookingStatusEnum.Failed
                    : BookingStatusEnum.Success;

                return new CheckStatusResponse { Status = booking.Status };
            }

            private string GenerateBookingCode()
            {
                const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                return new string(Enumerable.Repeat(chars, 6)
                    .Select(s => s[Random.Shared.Next(s.Length)]).ToArray());
            }
        }
    }
}
