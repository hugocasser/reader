using System.Reflection;
using BusinessLogicLayer.Abstractions.Configurations;
using BusinessLogicLayer.Abstractions.Services;
using BusinessLogicLayer.Abstractions.Services.AuthServices;
using BusinessLogicLayer.Abstractions.Services.DataServices;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.AuthServices;
using BusinessLogicLayer.Services.DataServices;
using FluentValidation;
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

    private static IServiceCollection UseEmailServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions();
        serviceCollection.AddTransient<IEmailConfirmMessageService, SendConfirmMessageEmailService>();
        serviceCollection.AddTransient<IEmailSenderService, EmailSenderService>();
        
        return serviceCollection;
    }

    private static IServiceCollection UseEmailServices(this IServiceCollection serviceCollection ,IEmailMessageSenderConfiguration configuration)
    {
        
        serviceCollection.AddSingleton(configuration);
        serviceCollection.AddTransient<IEmailConfirmMessageService, SendConfirmMessageEmailService>();
        serviceCollection.AddTransient<IEmailSenderService, EmailSenderService>();
        return serviceCollection;
    }
    
    private static IServiceCollection UseAuthenticationServices(this IServiceCollection serviceCollection,
        ITokenGenerationConfiguration configuration)
    {
        serviceCollection.AddSingleton(configuration);
        serviceCollection.AddScoped<IAuthTokenGeneratorService, JwtTokenGeneratorService>();
        serviceCollection.AddScoped<IRefreshTokenGeneratorService, RefreshTokenGeneratorService>();
        
        return serviceCollection;
    }

    public static IServiceCollection AddServices(this IServiceCollection serviceCollection,
        ITokenGenerationConfiguration configuration, IEmailMessageSenderConfiguration emailConfiguration)
    {
        serviceCollection.UseDataServices();
        serviceCollection.UseAuthenticationServices();
        serviceCollection.UseEmailServices();
        serviceCollection.UseValidation();
        
        return serviceCollection;
    }

    private static IServiceCollection UseValidation(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        return serviceCollection;
    }

    private static IServiceCollection AddTokenGenerationOptions
        (this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions<TokenGenerationOptions>()
            .BindConfiguration(nameof(TokenGenerationOptions))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return serviceCollection;
    }
    
    private static IServiceCollection AddEmailMessageSenderOptions
        (this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions<EmailMessageSenderOptions>()
            .BindConfiguration(nameof(EmailMessageSenderOptions))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        return serviceCollection;
    }
    
    private static IServiceCollection AddOptions
        (this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddTokenGenerationOptions()
            .AddEmailMessageSenderOptions();
    }
}