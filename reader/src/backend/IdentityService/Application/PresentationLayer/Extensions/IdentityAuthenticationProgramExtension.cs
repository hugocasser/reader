using System.Text;
using MicrosoftOptions = Microsoft.Extensions.Options.Options;
using BusinessLogicLayer.Options;
using DataAccessLayer.Models;
using DataAccessLayer.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace PresentationLayer.Extensions;

public static class IdentityAuthenticationProgramExtension
{
    public static IServiceCollection AddUsersIdentity(this IServiceCollection serviceCollection)
    {

        serviceCollection.AddIdentity<User, UserRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<UsersDbContext>()
            .AddDefaultTokenProviders()
            .AddRoles<UserRole>();

        return serviceCollection;
    }
    
    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var tokenOptions = new TokenGenerationOptions();
        configuration.GetSection(nameof(TokenGenerationOptions)).Bind(tokenOptions);
        serviceCollection.AddSingleton(MicrosoftOptions.Create(tokenOptions));
        
        serviceCollection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options => options
                .SetValidationTokenOptions(tokenOptions)
            );

        return serviceCollection;
    }
    
    private static JwtBearerOptions SetValidationTokenOptions(this JwtBearerOptions options, TokenGenerationOptions tokenGenerationOptions)
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateActor = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            RequireExpirationTime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenGenerationOptions.Issuer,
            ValidAudience = tokenGenerationOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenGenerationOptions.Key))
        };

        return options;
    }
}