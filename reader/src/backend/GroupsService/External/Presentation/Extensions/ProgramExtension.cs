using Application;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Options;
using Infrastructure.Persistence;
using Mapster;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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
            .AddDbContext(databaseOptions)
            .AddRepositories()
            .AddApplication()
            .AddSwagger()
            .AddOptions()
            .AddCors(options => options.ConfigureAllowAllCors())
            .AddEndpointsApiExplorer()
            .AddFluentValidationAutoValidation()
            .AddControllers();
            
        builder.Services.AddMapster();
        
        return builder;
    }
    
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
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
        var servicesProvider = scope.ServiceProvider;
        
        try
        {
            var readDbContext = servicesProvider.GetRequiredService<ReadDbContext>();
            var writeDbContext = servicesProvider.GetRequiredService<WriteDbContext>();
            await readDbContext.Database.MigrateAsync();
            await writeDbContext.Database.MigrateAsync();
            await app.RunAsync();
        }
        catch (Exception e)
        {
            //TODO: handle exceptions
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
}