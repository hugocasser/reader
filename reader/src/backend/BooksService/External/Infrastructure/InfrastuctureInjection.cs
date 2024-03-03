using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Domain.Models;
using Infrastructure.BackgroundJobs;
using Infrastructure.Options;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Quartz;

namespace Infrastructure;

public static class InfrastuctureInjection
{
    public static IServiceCollection AddDbContext(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient, CustomMongoClient>();
        services.AddSingleton<MongoDbContext>();
        services.AddOptions();
        
        return services;
    }
    
    public static IServiceCollection AddKafkaProducer(this IServiceCollection services)
    {
        services.AddScoped<IKafkaProducerService<Book>, KafkaProducerService>();
        services.AddOptions<KafkaProducerOptions>()
            .BindConfiguration(nameof(KafkaProducerOptions))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBooksRepository, BooksRepository>();
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        services.AddScoped<IAuthorsRepository, AuthorsRepository>();
        services.AddScoped<IEventsRepository<Book>, EventsRepository>();
        services.Decorate<IBooksRepository, DecoratedBooksRepository>();
        
        return services;
    }

    public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessEventsJob));
            configure
                .AddJob<ProcessEventsJob>(jobKey)
                .AddTrigger(trigger =>
                    trigger.ForJob(jobKey)
                        .WithSimpleSchedule(schedule =>
                            schedule.WithIntervalInSeconds(50).RepeatForever())); 
            configure.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();
        
        return services;
    }
    
    private static IServiceCollection AddOptions(this IServiceCollection services)
    {
        services.AddOptions<MongoOptions>()
            .BindConfiguration(nameof(MongoOptions))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        return services;
    }
}