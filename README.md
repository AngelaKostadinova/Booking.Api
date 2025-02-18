# Booking.Api

A booking API for managing hotel and flight reservations. Supports hotel-only bookings, flight+hotel packages, and last-minute hotel deals.

## Configuration

### Development Secrets
The application uses .NET User Secrets for managing sensitive configuration in development. To set up the required secrets:

1. Initialize user secrets for the project:
```bash
cd Booking.Api
dotnet user-secrets init
```

2. Add the required secrets:
```bash
dotnet user-secrets set "ApiKey" "bGmSysSfLBFWGfGoYj4DFHa00aMQVVgGg3eH8EurKIqYHb7RRbj4EV2GaTTJxzDEuJjm8wP9uRWSKU2Jac6XTICbeH8dKJbB4LxeJ03JUiRArlhrCKm2WcmEeZ2oJt8a"
```

### API Authentication
All API endpoints require API key authentication. The API key verifies the identity of the caller but does not provide different levels of access.

#### Using Swagger UI
1. Open the Swagger UI
2. Click the "Authorize" button at the top of the page
3. Enter your API key in the popup dialog
4. Click "Authorize"
5. All subsequent requests will include the API key

#### Using HTTP Clients
Include the API key in the request headers:
```http
X-Api-Key: bGmSysSfLBFWGfGoYj4DFHa00aMQVVgGg3eH8EurKIqYHb7RRbj4EV2GaTTJxzDEuJjm8wP9uRWSKU2Jac6XTICbeH8dKJbB4LxeJ03JUiRArlhrCKm2WcmEeZ2oJt8a
```

## API Endpoints

### Search
```http
POST /api/search
Content-Type: application/json
X-Api-Key: {your-api-key}

{
    "destination": "BCN",
    "departureAirport": "LHR",
    "fromDate": "2024-03-19",
    "toDate": "2024-03-21"
}
```

### Book
```http
POST /api/book
Content-Type: application/json
X-Api-Key: {your-api-key}

{
    "optionCode": "ABC123",
    "searchReq": {
        "destination": "BCN",
        "departureAirport": "LHR",
        "fromDate": "2024-03-20",
        "toDate": "2024-03-25"
    }
}
```

### Check Status
```http
GET /api/status/{bookingCode}
X-Api-Key: {your-api-key}
```

## Validation Rules
- Destination is required
- FromDate must not be in the past
- ToDate must be after FromDate
- DepartureAirport (if provided) must be a 3-letter IATA code