using Application;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Options;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Mapster;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Presentation.Hubs;
using Presentation.Middleware;
using MicrosoftOptions = Microsoft.Extensions.Options.Options;

namespace Presentation.Extensions;

public static class ProgramExtension
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        var databaseOptions = new DbOptions();
        builder.Configuration.GetSection(nameof(DbOptions)).Bind(databaseOptions);
        builder.Services.AddSingleton(MicrosoftOptions.Create(databaseOptions));
        builder.Services
            .UseClaimsServices()
            .AddRedisCache(builder.Configuration)
            .AddDbContext(databaseOptions)
            .AddRepositories()
            .AddApplication()
            .AddIdentity(builder.Configuration)
            .AddSwagger()
            .AddOptions()
            .AddTokenOptions()
            .AddElasticOptions()
            .AddRedisOptions()
            .AddCors(options => options.ConfigureAllowAllCors())
            .AddEndpointsApiExplorer()
            .AddFluentValidationAutoValidation()
            .AddControllers();

        builder.Services.AddSignalR();
        builder.Services.AddMapster();
        builder.Services.AddGrpc();
        
        builder.AddLoggingServices();
        
        return builder;
    }

    private static IServiceCollection UseClaimsServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        
        return services;
    }
    
    private static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder webApplication)
    {
        webApplication.UseMiddleware<CustomExceptionHandlerMiddleware>();
        
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "groups v1");
                c.RoutePrefix = "swagger";
            });
        }
        
        app.UseExceptionHandler(new ExceptionHandlerOptions()
        {
            AllowStatusCode404Response = true,
            ExceptionHandlingPath = "/error"
        });
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("AllowAll");
        app.MapControllers();
        app.MapHub<NotesHub>("/notesHub");
        app.MapGrpcService<GrpcUsersService>();
        
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
    
    public static async Task<WebApplication> RunApplicationAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        
        try
        {
            var readDbContext = serviceProvider.GetRequiredService<ReadDbContext>();
            var writeDbContext = serviceProvider.GetRequiredService<WriteDbContext>();
            await readDbContext.Database.MigrateAsync();
            await writeDbContext.Database.MigrateAsync();
            await app.RunAsync();
        }
        catch (Exception ex)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Host terminated unexpectedly");
            throw;
        }
        
        return app;
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
        });
        
        return serviceCollection;
    }

    private static IServiceCollection AddRedisOptions(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddOptions<RedisOptions>()
            .BindConfiguration(nameof(RedisOptions))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        return serviceCollection;
    }
}