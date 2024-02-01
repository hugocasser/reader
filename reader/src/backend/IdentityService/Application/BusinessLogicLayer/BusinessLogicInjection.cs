using System.Reflection;
using BusinessLogicLayer.Abstractions.Services.AuthServices;
using BusinessLogicLayer.Abstractions.Services.DataServices;
using BusinessLogicLayer.Options;
using BusinessLogicLayer.Services.AuthServices;
using BusinessLogicLayer.Services.DataServices;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer;

public static class BusinessLogicInjection
{
    private static IServiceCollection UseDataServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IUsersService, UsersService>()
            .AddScoped<IRolesService, RolesService>()
            .AddScoped<IRefreshTokensService, RefreshTokensService>();

        return serviceCollection;
    }

    private static IServiceCollection UseEmailServices(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddEmailSenderConfiguration(configuration);
        serviceCollection.AddSingleton(configuration);
        serviceCollection.AddTransient<IEmailConfirmMessageService, SendConfirmMessageEmailService>();
        serviceCollection.AddTransient<IEmailSenderService, EmailSenderService>();
        
        return serviceCollection;
    }

    private static IServiceCollection UseAuthenticationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAuthTokenGeneratorService, JwtTokenGeneratorService>();
        serviceCollection.AddScoped<IRefreshTokenGeneratorService, RefreshTokenGeneratorService>();

        return serviceCollection;
    }

    public static IServiceCollection AddServices
        (this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.UseDataServices();
        serviceCollection.UseAuthenticationServices();
        serviceCollection.UseEmailServices(configuration);
        serviceCollection.UseValidation();

        return serviceCollection;
    }

    private static IServiceCollection UseValidation(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        return serviceCollection;
    }

    private static IServiceCollection AddTokenGenerationOptions
        (this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection.Configure<TokenGenerationOptions>(options =>
        {
            configuration.GetSection(nameof(TokenGenerationOptions))
                .Bind(options);
        });
    }
    
    private static IServiceCollection AddEmailMessageSenderOptions
        (this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection.Configure<EmailMessageSenderOptions>(
            configuration.GetSection(nameof(EmailMessageSenderOptions))
        );
    }
    
    private static IServiceCollection AddEmailSenderConfiguration
        (this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection
            .AddTokenGenerationOptions(configuration)
            .AddEmailMessageSenderOptions(configuration);
    }

}