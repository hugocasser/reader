using System.Reflection;
using BusinessLogicLayer;
using DataAccessLayer;
using DataAccessLayer.Persistence;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using PresentationLayer.Common;
using PresentationLayer.Middleware;
using PresentationLayer.Options;

namespace PresentationLayer.Extensions;

public static class ProgramExtensions
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddDbContext()
            .AddRepositories()
            .AddAdminSeeder()
            .AddUsersIdentity()
            .AddServices()
            .AddJwtAuthentication()
            .AddSwagger()
            .AddCors(options => options.ConfigureAllowAllCors())
            .AddEndpointsApiExplorer()
            .AddControllers();
        
        builder.AddLoggingServices();
        
        return builder;
    }
    
    private static IServiceCollection AddSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });

        return serviceCollection;
    }

    private static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder webApplication)
    {
        webApplication.UseMiddleware<CustomExceptionHandler>();
        
        return webApplication;
    }
    public static async Task<WebApplication> RunApplicationAsync(this WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        
        try
        {
            var usersDbContext = serviceProvider.GetRequiredService<UsersDbContext>();
            await usersDbContext.Database.MigrateAsync();
            if (!usersDbContext.Users.Any())
            {
                var adminOptions = serviceProvider.GetRequiredService<IOptions<AdminSeederOptions>>();
                await usersDbContext.AddAsync(DataSeeder.SeedAdmin(adminOptions));
                await usersDbContext.AddRangeAsync(DataSeeder.SeedUserRoles(adminOptions));
                await usersDbContext.AddAsync(DataSeeder.SeedAdminWithRoles(adminOptions));
            }
            await webApplication.RunAsync();
        }
        catch (Exception ex)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Host terminated unexpectedly");
        }

        return webApplication;
    }
    
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        app.UseCustomExceptionHandler();
        app.UseLoggingDependOnEnvironment();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiDemo v1");
                c.RoutePrefix = "swagger";
            });
        }
        
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiDemo v1");
            c.RoutePrefix = "swagger";
        });
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("AllowAll");
        app.MapControllers();
        
        return app;
    }
    
    private static CorsOptions ConfigureAllowAllCors(this CorsOptions options)
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        });

        return options;
    }
    
    private static IServiceCollection AddAdminSeeder
        (this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions<AdminSeederOptions>()
            .BindConfiguration(nameof(AdminSeederOptions))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        return serviceCollection;
    }
}