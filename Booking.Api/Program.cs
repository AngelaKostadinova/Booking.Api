using Booking.Api.Middleware;
using Booking.Application.Interfaces;
using Booking.Application.Repositories;
using Booking.Application.Services;
using Booking.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register HttpClient
builder.Services.AddHttpClient();

// Register IExternalApiService and its implementation
builder.Services.AddScoped<IExternalApiService, ExternalApiService>();

// Register IManagerFactory and its implementation
builder.Services.AddScoped<IBookingManagerFactory, BookingManagerFactory>();
builder.Services.AddSingleton<ISearchRepository, InMemorySearchRepository>();
builder.Services.AddSingleton<IBookingRepository, InMemoryBookingRepository>();

// Register validators
builder.Services.AddTransient<ISearchRequestValidator, SearchRequestValidator>();

builder.Services.AddScoped<HotelOnlyManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();