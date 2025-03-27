using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Data;
using Microsoft.Extensions.Logging;


var connectionString = Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_DefaultConnection");


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true;
});


// Add controller services
builder.Services.AddControllers();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Logging
builder.Services.AddLogging();

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();  // Remove default providers
    logging.AddConsole();      // Ensure console logging is enabled
    logging.AddDebug();        // Debug output (useful for local testing)
    logging.SetMinimumLevel(LogLevel.Information);  // Set minimum log level
});



// Enable CORS to allow cross-origin requests
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty)),
            ValidateLifetime = true
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Show detailed errors in development
    app.UseSwagger(); // Enable Swagger
    app.UseSwaggerUI(); // Enable Swagger UI
}

app.UseCors("AllowAllOrigins"); // Enable CORS with the defined policy

app.UseRouting();
app.UseHttpsRedirection(); // Force HTTPS
app.UseAuthentication(); // Enable Authentication Middleware
app.UseAuthorization(); // Enable Authorization Middleware

// Make sure the order is correct here:
app.MapControllers(); // Map controllers to handle incoming requests

app.Run(); // Run the application


