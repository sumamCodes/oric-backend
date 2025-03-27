//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;
//using WebApi.Data;
//using Microsoft.Extensions.Logging;


//var connectionString = Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_DefaultConnection");


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
//    options.JsonSerializerOptions.WriteIndented = true;
//});


//// Add controller services
//builder.Services.AddControllers();

//// Add Swagger for API documentation
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// Add Logging
//builder.Services.AddLogging();

//builder.Services.AddLogging(logging =>
//{
//    logging.ClearProviders();  // Remove default providers
//    logging.AddConsole();      // Ensure console logging is enabled
//    logging.AddDebug();        // Debug output (useful for local testing)
//    logging.SetMinimumLevel(LogLevel.Information);  // Set minimum log level
//});



//// Enable CORS to allow cross-origin requests
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAllOrigins", builder =>
//        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
//});


//var jwtKey = Environment.GetEnvironmentVariable("Jwt:Key")
//          ?? Environment.GetEnvironmentVariable("APPSETTING_Jwt:Key");

//if (string.IsNullOrEmpty(jwtKey))
//{
//    throw new Exception("JWT Key is missing from environment variables!");
//}

//// Configure JWT Authentication
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = Environment.GetEnvironmentVariable("Jwt:Issuer") ?? "default_issuer",
//            ValidAudience = Environment.GetEnvironmentVariable("Jwt:Audience") ?? "default_audience",
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
//        };
//    });

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage(); // Show detailed errors in development
//    app.UseSwagger(); // Enable Swagger
//    app.UseSwaggerUI(); // Enable Swagger UI
//}

//app.UseCors("AllowAllOrigins"); // Enable CORS with the defined policy

//app.UseRouting();
//app.UseHttpsRedirection(); // Force HTTPS
//app.UseAuthentication(); // Enable Authentication Middleware
//app.UseAuthorization(); // Enable Authorization Middleware

//// Make sure the order is correct here:
//app.MapControllers(); // Map controllers to handle incoming requests

//app.Run(); // Run the application


using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Data;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Fetch database connection string
var connectionString = Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_DefaultConnection")
                      ?? builder.Configuration.GetConnectionString("DefaultConnection");

// Add Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add Controllers & JSON Serialization
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true;
});

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Logging
builder.Logging.ClearProviders(); // Remove only default providers
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Fetch JWT credentials from environment variables
var jwtKey = Environment.GetEnvironmentVariable("Jwt__Key")
          ?? Environment.GetEnvironmentVariable("APPSETTING_Jwt:Key");

var jwtIssuer = Environment.GetEnvironmentVariable("Jwt__Issuer")
             ?? Environment.GetEnvironmentVariable("APPSETTING_Jwt:Issuer")
             ?? "default_issuer";

var jwtAudience = Environment.GetEnvironmentVariable("Jwt__Audience")
              ?? Environment.GetEnvironmentVariable("APPSETTING_Jwt:Audience")
              ?? "default_audience";

if (string.IsNullOrEmpty(jwtKey))
{
    throw new Exception("JWT Key is missing from environment variables!");
}

// Debug logs for environment variables
Console.WriteLine($"JWT Issuer: {jwtIssuer}");
Console.WriteLine($"JWT Audience: {jwtAudience}");
Console.WriteLine($"JWT Key Length: {jwtKey?.Length}");
Console.WriteLine($"Database Connection String: {connectionString}");



if (string.IsNullOrEmpty(jwtKey))
{
    throw new Exception("JWT Key is missing or empty in environment variables!");
}

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

var app = builder.Build();

// Enable Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable Middleware
app.UseCors("AllowAllOrigins");
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
