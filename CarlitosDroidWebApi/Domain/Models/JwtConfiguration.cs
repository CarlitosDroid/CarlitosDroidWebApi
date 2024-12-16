using System.Globalization;

namespace CarlitosDroidWebApi.Domain.Models;

public class JwtConfiguration
{
    public string Issuer { get; } = string.Empty;

    public string Secret { get; } = string.Empty;

    public string Audience { get; } = string.Empty;

    public int ExpireDays { get; }

    public JwtConfiguration(IConfiguration configuration, string hola)
    {
        var section = configuration.GetSection("JWT");
        Issuer = section[nameof(Issuer)] ?? Environment.GetEnvironmentVariable("JWT_ISSUER");
        Secret = section[nameof(Secret)] ?? Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
        Audience = section[nameof(Secret)] ?? Environment.GetEnvironmentVariable("JWT_AUDIENCE");

        var jwtExpiration = section[nameof(ExpireDays)] ?? Environment.GetEnvironmentVariable("JWT_EXPIRATION");
        ExpireDays = Convert.ToInt32(jwtExpiration, CultureInfo.InvariantCulture);
    }
}