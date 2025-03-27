using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Data;
using Microsoft.Extensions.Logging;

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






//using dotenv.net;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;
//using WebApi.Data;

//var builder = WebApplication.CreateBuilder(args);

//// ✅ Load environment variables from .env file
//DotNetEnv.Env.Load();



//// ✅ Get environment variables with error handling
//string jwtKey = Environment.GetEnvironmentVariable("JWT_KEY")
//    ?? throw new InvalidOperationException("❌ JWT_KEY is missing in .env file");
//string jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER")
//    ?? throw new InvalidOperationException("❌ JWT_ISSUER is missing in .env file");
//string jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")
//    ?? throw new InvalidOperationException("❌ JWT_AUDIENCE is missing in .env file");
//string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
//    ?? throw new InvalidOperationException("❌ CONNECTION_STRING is missing in .env file");

//// ✅ Logging environment variable values for debugging (masked for security)
//builder.Logging.AddConsole();
//builder.Logging.AddDebug();

//var loggerFactory = LoggerFactory.Create(loggingBuilder =>
//{
//    loggingBuilder.AddConsole();
//    loggingBuilder.AddDebug();
//});

//var logger = loggerFactory.CreateLogger<Program>();


//var jwtSecret = Environment.GetEnvironmentVariable("JWT_KEY");

//if (!string.IsNullOrEmpty(jwtSecret))
//{
//    byte[] keyBytes = Convert.FromBase64String(jwtSecret);
//    Console.WriteLine($"✅ JWT_SECRET Byte Length: {keyBytes.Length * 8} bits"); // Must be at least 128 bits
//    logger.LogInformation($"✅ JWT_SECRET Byte Length: {keyBytes.Length * 8} bits");

//    var key = new SymmetricSecurityKey(keyBytes);
//    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//}

//jwtSecret = jwtSecret.Trim(); 
//logger.LogInformation($"✅ JWT_SECRET Length: {jwtSecret.Length}");

//if (!string.IsNullOrEmpty(jwtSecret))
//{
//    Console.WriteLine($"✅ JWT_SECRET Value: {jwtSecret}"); // Ensure it's actually set
//    logger.LogInformation("✅ JWT_ISSUER: {Issuer}", jwtSecret);
//    Console.WriteLine($"✅ JWT_SECRET Length: {jwtSecret.Length}");
//    logger.LogInformation($"✅ JWT_SECRET Length: {jwtSecret.Length}");
//}
//else
//{
//    Console.WriteLine("❌ JWT_SECRET is missing or empty!");
//}

//logger.LogInformation("✅ Environment variables loaded successfully.");
//logger.LogInformation("✅ JWT_ISSUER: {Issuer}", jwtIssuer);
//logger.LogInformation("✅ JWT_AUDIENCE: {Audience}", jwtAudience);
//logger.LogInformation("✅ CONNECTION_STRING Loaded");

//// ✅ Configure Database Connection
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseNpgsql(connectionString));

//// ✅ Configure Authentication and Authorization
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidIssuer = jwtIssuer,
//            ValidAudience = jwtAudience,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true
//        };

//        options.Events = new JwtBearerEvents
//        {
//            OnAuthenticationFailed = context =>
//            {
//                logger.LogError("🔴 Authentication failed: {Message}", context.Exception.Message);
//                return Task.CompletedTask;
//            },
//            OnTokenValidated = context =>
//            {
//                logger.LogInformation("✅ Token validated successfully!");
//                return Task.CompletedTask;
//            }
//        };
//    });


//builder.Logging.ClearProviders();
//builder.Logging.AddConsole(); // Ensure console logging is enabled

//// ✅ Ensure Authorization is added
//builder.Services.AddAuthorization();

//// ✅ Add Controllers and JSON Configuration
//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
//    options.JsonSerializerOptions.WriteIndented = true;
//});

//// ✅ Enable Swagger for API Documentation
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// ✅ Enable CORS to allow cross-origin requests
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAllOrigins", policy =>
//        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
//});

//var app = builder.Build();

//// ✅ Configure Middleware Pipeline
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseCors("AllowAllOrigins");
//app.UseRouting();
//app.UseAuthentication(); // ✅ Ensure Authentication is applied
//app.UseAuthorization();  // ✅ Ensure Authorization is applied

//app.MapControllers();
//app.Run();



