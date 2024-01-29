using System.Reflection;
using DataAccessLayer.Abstractions.Configurations;
using DataAccessLayer.Abstractions.Repositories;
using DataAccessLayer.Persistence;
using DataAccessLayer.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer;

public static class DataAccessInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUsersRepository, UsersRepository>();
        serviceCollection.AddScoped<IUserRolesRepository, UserRolesRepository>();
        serviceCollection.AddScoped<IRefreshTokensRepository, RefreshTokensRepository>();
        
        return serviceCollection;
    }

    public static IServiceCollection AddDbContext(this IServiceCollection serviceCollection, IApplicationConfiguration configuration)
    {
        return serviceCollection.AddSqlServerDatabase(configuration.DatabaseConnectionString);
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