using Application.Abstractions.Repositories;
using Application.Abstractions.Services.Cache;
using Application.BackgroundJobs;
using Application.Services;
using Hangfire;
using MicrosoftOptions = Microsoft.Extensions.Options.Options;
using Infrastructure.Interceptor;
using Infrastructure.Options;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastructure;

public static class InfrastructureInjection
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, DbOptions dbOptions)
    {
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddReadDbContext(dbOptions);
        services.AddWriteDbContext(dbOptions);
        
        return services;
    }
    public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        var redisOptions = new RedisOptions();
        configuration.GetSection(nameof(RedisOptions)).Bind(redisOptions);
        services.AddSingleton(MicrosoftOptions.Create(redisOptions));
        
        services.AddSingleton<IConnectionMultiplexer, ConnectionMultiplexer>(provider => 
            provider.GetService<ConnectionMultiplexer>() 
            ?? ConnectionMultiplexer.Connect(redisOptions.ConnectionString));
        
        services.AddSingleton<IRedisCacheService, RedisCacheService>();
        
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

    private static void AddHangfireProcesses()
    {
        RecurringJob.AddOrUpdate<BackgroundCacheService>(
            "PushNotes",
            x => x.PushNotes(),
            Cron.Minutely);
    }
    
    private static IServiceCollection AddDbOptions(this IServiceCollection services)
    {
        services.AddOptions<DbOptions>()
            .BindConfiguration(nameof(DbOptions))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        return services;
    }
}