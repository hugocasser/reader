using BusinessLogicLayer.Abstractions.Configurations;
using BusinessLogicLayer.Abstractions.Services;
using BusinessLogicLayer.Abstractions.Services.DataServices;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.AuthServices;
using BusinessLogicLayer.Services.DataServices;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer;

public static class BusinessLogicInjection
{
    public static IServiceCollection UseDataServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IUsersService, UsersService>()
            .AddScoped<IRolesService, RolesService>()
            .AddScoped<IRefreshTokensService, RefreshTokensService>();
        
        return serviceCollection;
    }

    public static IServiceCollection UseEmailServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }

    public static IServiceCollection UseAuthenticationServices(this IServiceCollection serviceCollection,
        ITokenGenerationConfiguration configuration)
    {
        serviceCollection.AddSingleton(configuration);
        serviceCollection.AddScoped<IAuthTokenGeneratorService, JwtTokenGeneratorService>();
        serviceCollection.AddScoped<IRefreshTokenGeneratorService, RefreshTokenGeneratorService>();
        
        return serviceCollection;
    }

    public static IServiceCollection AddServices(this IServiceCollection serviceCollection,
        ITokenGenerationConfiguration configuration)
    {
        serviceCollection.UseDataServices();
        serviceCollection.UseAuthenticationServices(configuration);
        return serviceCollection;
    }
}