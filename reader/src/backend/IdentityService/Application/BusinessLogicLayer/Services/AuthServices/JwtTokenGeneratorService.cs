using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessLogicLayer.Abstractions.Configurations;
using BusinessLogicLayer.Abstractions.Services;
using BusinessLogicLayer.Abstractions.Services.AuthServices;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogicLayer.Services.AuthServices;

public class JwtTokenGeneratorService : IAuthTokenGeneratorService
{
    private readonly ITokenGenerationConfiguration _configuration;

    public JwtTokenGeneratorService(ITokenGenerationConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(Guid userId, string userEmail, IEnumerable<string> roles)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, userEmail),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture))
        };
        foreach (var role in roles)
        {
            claims.Add(new Claim("role:", role));
        }
        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            issuer: _configuration.Issuer,
            audience: _configuration.Audience,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Key)),
                SecurityAlgorithms.HmacSha512Signature));

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}