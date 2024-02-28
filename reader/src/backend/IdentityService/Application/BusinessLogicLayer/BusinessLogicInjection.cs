using System.Reflection;
using BusinessLogicLayer.Abstractions.Services.AuthServices;
using BusinessLogicLayer.Abstractions.Services.DataServices;
using BusinessLogicLayer.Abstractions.Services.Grpc;
using BusinessLogicLayer.Options;
using BusinessLogicLayer.Services.AuthServices;
using BusinessLogicLayer.Services.DataServices;
using BusinessLogicLayer.Services.Grpc;
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

    private static IServiceCollection UseAuthenticationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAuthTokenGeneratorService, JwtTokenGeneratorService>();
        serviceCollection.AddScoped<IRefreshTokenGeneratorService, RefreshTokenGeneratorService>();

        return serviceCollection;
    }

    public static IServiceCollection AddServices
        (this IServiceCollection serviceCollection)
    {
        serviceCollection.UseDataServices();
        serviceCollection.UseAuthenticationServices();
        serviceCollection.UseEmailServices();
        serviceCollection.UseValidation();
        serviceCollection.AddScoped<IGrpcUserService, GrpcUsersService>();
        
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
            .ValidateOnStart();
        
        return serviceCollection;
    }
    
    private static IServiceCollection AddEmailMessageSenderOptions
        (this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions<EmailMessageSenderOptions>()
            .BindConfiguration(nameof(EmailMessageSenderOptions))
            .ValidateOnStart();
        
        return serviceCollection;
    }

    private static IServiceCollection AddGrpcOptions(this IServiceCollection services)
    {
        services.AddOptions<GrpcOptions>()
            .BindConfiguration(nameof(GrpcOptions))
            .ValidateOnStart();
        
        return services;
    }
    
    private static IServiceCollection AddOptions
        (this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddTokenGenerationOptions()
            .AddEmailMessageSenderOptions()
            .AddGrpcOptions();
    }

}