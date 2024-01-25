using System.Diagnostics;
using System.Reflection;
using BusinessLogicLayer;
using DataAccessLayer;
using DataAccessLayer.Persistence;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.OpenApi.Models;
using PresentationLayer.Configurations;

namespace PresentationLayer.Extentions;

public static class ProgramExtensions
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        var applicationConfiguration = new ApplicationConfiguration(builder.Configuration);
        var tokenGenerationConfiguration = new TokenGenerationConfiguration(builder.Configuration);

        builder.Services
            .AddDbContext(applicationConfiguration)
            .AddRepositories()
            .AddUsersIdentity()
            .AddServices(tokenGenerationConfiguration)
            .AddJwtAuthentication(tokenGenerationConfiguration)
            .AddSwagger()
            .AddCors(options => options.ConfigureAllowAllCors())
            .AddEndpointsApiExplorer()
            .AddControllers();

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
    
    public static async Task<WebApplication> RunApplicationAsync(this WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        try
        {
            var usersDbContext = serviceProvider.GetRequiredService<UsersDbContext>();
            await usersDbContext.Database.EnsureCreatedAsync();

            await webApplication.RunAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error: " + ex.Message + "\n" + 
                            "StackTrace: " + ex.StackTrace + "\n" +
                            "Source: " + ex.Source);
        }

        return webApplication;
    }
    
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        app.UseExceptionHandler("/error");
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiDemo v1");
            c.RoutePrefix = "swagger";
        });

        app.UseExceptionHandler("/error");

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

}