using Booking.Application.DTOs.Requests;
using Booking.Application.DTOs.Responses;
using Booking.Application.Interfaces;
using Booking.Application.Interfaces.Repositories;
using Booking.Application.Utils;
using Booking.Domain.Enums;

namespace Booking.Application.Services
{
    namespace WebApi.Services
    {
        public abstract class BaseBookingManager : IBookingManager
        {
            protected readonly IExternalApiService _externalApiService;
            protected readonly ISearchRepository _searchRepository;
            protected readonly IBookingRepository _bookingRepository;

            protected BaseBookingManager(
                IExternalApiService externalApiService,
                ISearchRepository searchRepository,
                IBookingRepository bookingRepository)
            {
                _externalApiService = externalApiService;
                _searchRepository = searchRepository;
                _bookingRepository = bookingRepository;
            }

            public virtual async Task<SearchResponse> Search(SearchRequest request)
            {
                var searchResponse = await PerformSearch(request);
                var searchId = Guid.NewGuid().ToString();
                await _searchRepository.StoreSearchResultsAsync(searchId, searchResponse);
                return searchResponse;
            }

            protected abstract Task<SearchResponse> PerformSearch(SearchRequest request);

            public async Task<BookResponse> Book(BookRequest request)
            {
                var option = await _searchRepository.GetOptionByCodeAsync(request.OptionCode);

                var bookingCode = CodeGenerator.GenerateCode();
                var sleepTime = Random.Shared.Next(30, 60);

                var bookingInfo = new BookingInfo
                {
                    BookingCode = bookingCode,
                    BookingTime = DateTime.Now,
                    SleepTime = sleepTime,
                    Status = BookingStatusEnum.Pending,
                    SearchType = GetType().Name,
                    Option = option
                };

                await _bookingRepository.StoreBookingAsync(bookingInfo);

                return new BookResponse
                {
                    BookingCode = bookingCode,
                    BookingTime = bookingInfo.BookingTime
                };
            }

            public async Task<CheckStatusResponse> CheckStatus(CheckStatusRequest request)
            {
                var booking = await _bookingRepository.GetBookingAsync(request.BookingCode);
                var elapsedTime = (DateTime.Now - booking.BookingTime).TotalSeconds;

                if (elapsedTime < booking.SleepTime)
                {
                    return new CheckStatusResponse { Status = BookingStatusEnum.Pending };
                }

                var newStatus = booking.SearchType == nameof(LastMinuteHotelsManager)
                    ? BookingStatusEnum.Failed
                    : BookingStatusEnum.Success;

                await _bookingRepository.UpdateBookingStatusAsync(booking.BookingCode, newStatus);

                return new CheckStatusResponse { Status = newStatus };
            }
        }
    }
}
