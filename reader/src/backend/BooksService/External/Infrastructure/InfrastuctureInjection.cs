using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Options;
using Application.Services;
using Domain.Models;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

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
        services.AddScoped<IKafkaProducerService<Book>, KafkaProducerBooksService>();
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