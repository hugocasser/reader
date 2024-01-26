using BusinessLogicLayer.Abstractions.Configurations;
using BusinessLogicLayer.Abstractions.Services;
using BusinessLogicLayer.Abstractions.Services.AuthServices;
using BusinessLogicLayer.Abstractions.Services.DataServices;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.AuthServices;
using BusinessLogicLayer.Services.DataServices;
using BusinessLogicLayer.Validation.Validators;
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
        serviceCollection.UseAuthenticationServices(configuration);
        serviceCollection.UseEmailServices(emailConfiguration);
        serviceCollection.UseValidation();
        
        return serviceCollection;
    }

    private static IServiceCollection UseValidation(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddValidatorsFromAssemblyContaining<GiveRoleToUserValidator>();
        serviceCollection.AddValidatorsFromAssemblyContaining<LoginValidator>();
        serviceCollection.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();
        serviceCollection.AddValidatorsFromAssemblyContaining<UpdateUserValidator>();
        serviceCollection.AddValidatorsFromAssemblyContaining<UpdateAuthTokenValidator>();
        return serviceCollection;
    }
}