using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CarlitosDroidWebApi.Extensions;

public static class SwaggerConfiguration
{

    // Define security scheme
    public static OpenApiSecurityScheme Scheme => new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer",
        Reference = new OpenApiReference
        {
            Id = "Bearer",
            Type = ReferenceType.SecurityScheme,
        },
    };

    // Function to enable Authorization using swagger
    public static void Configure(SwaggerGenOptions option)
    {
        option.ResolveConflictingActions(apiDesc => apiDesc.First());
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

        // Define security scheme
        option.AddSecurityDefinition(Scheme.Reference.Id, Scheme);
        
        // Apply security to endpoints
        option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            { Scheme, Array.Empty<string>() },
        });
    }
}