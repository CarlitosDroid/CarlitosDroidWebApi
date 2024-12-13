using CarlitosDroidWebApi.Claims;
using CarlitosDroidWebApi.Data;
using CarlitosDroidWebApi.Domain.Models;
using CarlitosDroidWebApi.Domain.Service;
using CarlitosDroidWebApi.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Load environment-specific configurations
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var apikey = builder.Configuration["AppSettings:APIKey"] ?? Environment.GetEnvironmentVariable("MY_API_KEY");
var dbStringConnection = builder.Configuration.GetConnectionString("DefaultConnection") ?? Environment.GetEnvironmentVariable("DATABASE_URL");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAuthorization();
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(dbStringConnection)
);
builder.Services.AddScoped<PasswordHasher>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Authentication
builder.Services.AddTransient<JwtConfiguration>();
builder.Services.AddTransient<TokenService>();
builder.Services.AddTransient<AppUserClaimsPrincipal>();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerGen(SwaggerConfiguration.Configure); // Enable Authorize in Swagger for easy testing
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "CarlitosDroid Web Api Definition v1")
    );
}

//app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();