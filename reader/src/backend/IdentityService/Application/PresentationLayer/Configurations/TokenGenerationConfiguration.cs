using BusinessLogicLayer.Abstractions.Configurations;
using DataAccessLayer.Exceptions;

namespace PresentationLayer.Configurations;

public class TokenGenerationConfiguration : ITokenGenerationConfiguration
    
{
    public string Issuer { get; }
    public string Audience { get; }
    public string Key { get; }
    
    public TokenGenerationConfiguration(IConfiguration configuration)
    {
        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TEST_ConnectionString")))
        {
            Issuer = "identity";
            Audience = "identity";
            Key = "0567e065-e6a5-4c25-aa36-e8304303b14b";
            
            return;
        }
        
        Issuer = configuration["Jwt:Issuer"] ?? throw new UserSecretsInvalidException("setup-jwt-issuer-secret");
        Audience = configuration["Jwt:Audience"] ?? throw new UserSecretsInvalidException("setup-jwt-audience-secret");
        Key = configuration["Jwt:Key"] + "dd84be26-7824-4bed-8ec2-09287a84bbad" ?? throw new UserSecretsInvalidException("setup-jwt-key-secrets");
    }
}