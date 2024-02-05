using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Presentation.Options;
using MicrosoftOptions = Microsoft.Extensions.Options.Options;

namespace Presentation.Extensions;

public static class IdentityProgramExtension
{
    public static IServiceCollection AddIdentity
        (this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var tokenOptions = new TokenOptions();
        configuration.GetSection(nameof(TokenOptions)).Bind(tokenOptions);
        serviceCollection.AddSingleton(MicrosoftOptions.Create(tokenOptions));
        serviceCollection.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
                options.SetTokenOptions(tokenOptions));

        return serviceCollection;
    }

    private static JwtBearerOptions SetTokenOptions
        (this JwtBearerOptions jwtBearerOptions, TokenOptions tokenOptions)
    {
        jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateActor = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            RequireExpirationTime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Key))
        };

        return jwtBearerOptions;
    }
}