using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarlitosDroidWebApi.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace CarlitosDroidWebApi.Domain.Service;

// Create the Token Generation Service
public class TokenService
{
    private readonly JwtConfiguration _config;

    public TokenService(JwtConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.UserID),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            // Add more claims if needed
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config.Issuer,
            audience: _config.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(_config.ExpireDays),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}