using Application.Abstractions.Repositories;
using Application.Abstractions.Services.Cache;
using Hangfire;
using MicrosoftOptions = Microsoft.Extensions.Options.Options;
using Infrastructure.BackgroundJobs;
using Infrastructure.Interceptor;
using Infrastructure.Options;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using StackExchange.Redis;

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
    
    private static IServiceCollection AddJobs(this IServiceCollection services)
    {
        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));
            configure
                .AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(trigger =>
                    trigger.ForJob(jobKey)
                        .WithSimpleSchedule(schedule =>
                            schedule.WithIntervalInSeconds(10).RepeatForever())); 
            configure.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();
        
       return services;
    }

    private static void AddHangfireProcesses()
    {
        RecurringJob.AddOrUpdate<BackgroundCacheService>(
            "PushNotes",
            x => x.PushNotes(),
            Cron.Minutely);
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
        services.AddJobs();
        
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UserRepository>();
        services.AddScoped<IGroupsRepository, GroupsRepository>();
        services.AddScoped<INotesRepository, NotesRepository>();
        services.AddScoped<IBooksRepository, BooksRepository>();
        services.AddScoped<IUserBookProgressRepository, UserBookProgressRepository>();
        services.Decorate<INotesRepository, CashedNotesRepository>();
        
        return services;
    }
    
}