using Application.Abstractions.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Presentation.Options;

namespace Infrastructure;

public static class InfrastuctureInjection
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<MongoDbContext>();
        services.AddOptions(configuration);
        
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBooksRepository, BooksRepository>();
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        services.AddScoped<IAuthorsRepository, AuthorsRepository>();
        
        return services;
    }

    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptionsValidation();
        
        return services.Configure<MongoOptions>(options =>
        {
            configuration.GetSection(nameof(MongoOptions)).Bind(options);
        });
    }

    private static IServiceCollection AddOptionsValidation(this IServiceCollection services)
    {
        return services.AddSingleton<IValidateOptions<MongoOptions>, MongoOptionsValidator>();
    }
}