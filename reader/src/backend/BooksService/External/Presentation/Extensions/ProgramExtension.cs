using Application;
using FluentValidation.AspNetCore;
using Infrastructure;
using Mapster;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.OpenApi.Models;
using Presentation.Middlewares;

namespace Presentation.Extensions;

public static class ProgramExtension
{
    public static WebApplicationBuilder ConfigureBuilder
        (this WebApplicationBuilder builder)
    {
        builder.Services
            .AddDbContext()
            .AddRepositories()
            .AddServices()
            .AddTokenOptions()
            .AddElasticOptions()
            .AddKafkaProducer()
            .AddIdentity(builder.Configuration)
            .AddOptions()
            .AddBackgroundJobs()
            .AddFluentValidationAutoValidation()
            .AddSwagger()
            .AddCors(options => options.ConfigureAllowAllCors())
            .AddControllers();

        builder.Services
            .AddMapster();
        
        return builder.AddLoggingServices(); 
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
    
    private static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder webApplication)
    {
        webApplication.UseMiddleware<CustomExceptionHandlerMiddleware>();
        
        return webApplication;
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