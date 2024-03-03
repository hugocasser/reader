using System.Reflection;
using DataAccessLayer.Abstractions.Repositories;
using DataAccessLayer.Models;
using DataAccessLayer.Options;
using DataAccessLayer.Persistence;
using DataAccessLayer.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftOptions = Microsoft.Extensions.Options.Options;

namespace DataAccessLayer;

public static class DataAccessInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<UserManager<User>>();
        serviceCollection.AddScoped<RoleManager<UserRole>>();
        serviceCollection.AddScoped<IRefreshTokensRepository, RefreshTokensRepository>();

        return serviceCollection;
    }

    public static IServiceCollection AddDbContext
    (this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var databaseOptions = new DataBaseOption();
        configuration.GetSection(nameof(DataBaseOption)).Bind(databaseOptions);
        
        serviceCollection.AddSqlServerDatabase(databaseOptions.ConnectionString);
        
        return serviceCollection;
    }

    private static IServiceCollection AddSqlServerDatabase(this IServiceCollection serviceCollection,
        string connectionString)
    {
        serviceCollection.AddDbContext<UsersDbContext>(options =>
        {
            options.EnableDetailedErrors();
            options.UseSqlServer(
                connectionString,
                builder =>
                {
                    builder.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
        });

        return serviceCollection;
    }
    
}