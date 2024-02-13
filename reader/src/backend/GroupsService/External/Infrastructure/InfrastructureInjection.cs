using Infrastructure.Options;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureInjection
{
    private static IServiceCollection AddReadDbContext(this IServiceCollection services)
    {
        services.AddDbOptions();
        services.AddDbContext<ReadDbContext>();
        
        return services;
    }
    
    private static IServiceCollection AddWriteDbContext(this IServiceCollection services)
    {
        services.AddDbContext<WriteDbContext>();
        
        return services;
    }
    
    private static IServiceCollection AddDbOptions(this IServiceCollection services)
    {
        services.AddOptions<DbOptions>()
            .BindConfiguration(nameof(DbOptions))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        return services;
    }
    
    public static IServiceCollection AddDbContext(this IServiceCollection services)
    {
        services.AddReadDbContext();
        services.AddWriteDbContext();
        
        return services;
    }
    
    
}