using Booking.Application.DTOs.Requests;
using Booking.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

public class SearchRequestValidator : ISearchRequestValidator
{
    public void Validate(SearchRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.Destination))
            throw new ValidationException("Destination is required");

        if (request.FromDate.Date < DateTime.Now.Date)
            throw new ValidationException("Cannot search in the past");

        if (request.ToDate < request.FromDate)
            throw new ValidationException("ToDate must be after FromDate");

        ValidateDepartureAirport(request.DepartureAirport);
    }

    private void ValidateDepartureAirport(string? departureAirport)
    {
        if (!string.IsNullOrEmpty(departureAirport))
        {
            if (departureAirport.Length is not 3)
                throw new ValidationException("DepartureAirport must be a 3-letter IATA code");

            if (!departureAirport.All(char.IsLetter))
                throw new ValidationException("DepartureAirport must contain only letters");
        }
    }
}
