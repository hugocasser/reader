using Application.Abstractions.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Options;

namespace Infrastructure;

public static class InfrastuctureInjection
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<MongoDbContext>();
        services.AddMongoOptions(configuration);
        
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBooksRepository, BooksRepository>();
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        services.AddScoped<IAuthorsRepository, AuthorsRepository>();
        
        return services;
    }

    private static IServiceCollection AddMongoOptions(this IServiceCollection services, IConfiguration configuration)
    {
        return services.Configure<MongoOptions>(options =>
        {
            configuration.GetSection(nameof(MongoOptions)).Bind(options);
        });
    }
}