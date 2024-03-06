using System.Reflection;
using Application.Abstractions.Services.Cache;
using Application.BackgroundJobs;
using Application.PipelineBehaviors;
using Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Application;

public static class ApplicationInjection
{
    
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddRequestHandlers();
        services.AddValidators();
        services.AddPipelineBehaviors();
        services.AddJobs();
        services.AddCacheServices();
        
        return services;
    }
    
    private static IServiceCollection AddRequestHandlers(this IServiceCollection services)
    {
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        
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
                            schedule.WithIntervalInSeconds(60).RepeatForever())); 
            configure.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();
        
        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        return services;
    }
    private static IServiceCollection AddPipelineBehaviors(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ClaimsMapperPipelineBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        
        return services;
    }

    private static IServiceCollection AddCacheServices(this IServiceCollection services)
    {
        services.AddScoped<ICashedNotesService, CachedNotesService>();
        services.AddScoped<IRedisCacheService, RedisCacheService>();
        
        return services;
    }
}