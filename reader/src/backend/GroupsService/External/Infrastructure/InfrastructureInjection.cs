using Application.Abstractions.Repositories;
using Infrastructure.Interceptor;
using Infrastructure.Options;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public static class InfrastructureInjection
{
    private static IServiceCollection AddReadDbContext(this IServiceCollection services, DbOptions dbOption)
    {
        services.AddDbOptions();
        services.AddDbContext<ReadDbContext>(options =>
        {
            options.UseNpgsql(dbOption.ReadConnectionString);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
        
        return services;
    }
    
    private static IServiceCollection AddWriteDbContext(this IServiceCollection services, DbOptions dbOption)
    {
        services.AddDbContext<WriteDbContext>((sp, options) =>
        {
            var tenantInterceptor = sp.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>();
            options
                .UseNpgsql(dbOption.WriteConnectionString)
                .AddInterceptors(tenantInterceptor);
        });
        
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
    
    public static IServiceCollection AddDbContext(this IServiceCollection services, DbOptions dbOptions)
    {
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddReadDbContext(dbOptions);
        services.AddWriteDbContext(dbOptions);
        
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UserRepository>();
        services.AddScoped<IGroupsRepository, GroupsRepository>();
        services.AddScoped<INotesRepository, NotesRepository>();
        services.AddScoped<IBooksRepository, BooksRepository>();
        services.AddScoped<IUserBookProgressRepository, UserBookProgressRepository>();
        
        return services;
    }
    
}