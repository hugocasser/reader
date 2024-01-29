using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessLogicLayer.Abstractions.Configurations;
using BusinessLogicLayer.Abstractions.Services.AuthServices;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogicLayer.Services.AuthServices;

public class JwtTokenGeneratorService(ITokenGenerationConfiguration configuration) : IAuthTokenGeneratorService
{
    public string GenerateToken(Guid userId, string userEmail, IEnumerable<string> roles)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, userEmail),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture))
        };
        
        claims.AddRange(roles.Select(role => new Claim("role:", role)));
        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            issuer: configuration.Issuer,
            audience: configuration.Audience,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Key)),
                SecurityAlgorithms.HmacSha512Signature));

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}