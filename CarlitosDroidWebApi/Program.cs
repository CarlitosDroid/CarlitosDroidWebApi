using CarlitosDroidWebApi.Data;
using CarlitosDroidWebApi.Domain.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Load environment-specific configurations
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
var apikey = builder.Configuration["AppSettings:APIKey"] ?? Environment.GetEnvironmentVariable("MY_API_KEY");
var dbStringConnection = builder.Configuration.GetConnectionString("DefaultConnection") ?? Environment.GetEnvironmentVariable("DATABASE_URL");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddHealthChecks();
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(dbStringConnection)
);
builder.Services.AddScoped<UserService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "CarlitosDroid WEB API" });
});

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
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/healthz");
app.Run();