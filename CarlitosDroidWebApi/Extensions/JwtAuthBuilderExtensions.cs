using System.Text;
using CarlitosDroidWebApi.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CarlitosDroidWebApi.Extensions;

// Configure JWT Authentication
public static class JwtAuthBuilderExtensions
{
    public static AuthenticationBuilder AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfiguration = new JwtConfiguration(configuration);

        services.AddAuthorization();

        return services.AddAuthentication(authOptions =>
        {
            authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(jbOptions =>
        {
            jbOptions.SaveToken = true;
            jbOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfiguration.Issuer,
                ValidAudience = jwtConfiguration.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Secret)),
                RequireExpirationTime = true,
            };
            jbOptions.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    string authorization = context.Request.Headers["Authorization"];

                    if (string.IsNullOrEmpty(authorization))
                    {
                        context.NoResult();
                    }
                    else
                    {
                        context.Token = authorization.Replace("Bearer ", string.Empty);
                    }

                    return Task.CompletedTask;
                },
            };
        });
    }
}