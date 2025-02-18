using Booking.Api.Middleware;
using Booking.Application.Interfaces;
using Booking.Application.Interfaces.Repositories;
using Booking.Application.Repositories;
using Booking.Application.Services;
using Booking.Infrastructure.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Booking API", Version = "v1" });

    // Add security definition for API key
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        Name = "X-Api-Key",
        In = ParameterLocation.Header,
        Description = "API Key authentication using the 'X-Api-Key' header"
    });

    // Make API key required for all operations
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new string[] {}
        }
    });
});

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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking API");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();