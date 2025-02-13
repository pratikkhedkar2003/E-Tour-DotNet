
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using etour_api.Dtos;
using Microsoft.IdentityModel.Tokens;

namespace etour_api.Services.Impl;

public class JwtServiceImpl : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtServiceImpl(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(UserDto user)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.UserId),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
        );
#pragma warning restore CS8604 // Possible null reference argument.

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}