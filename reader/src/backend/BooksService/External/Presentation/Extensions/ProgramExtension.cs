using Application;
using FluentValidation.AspNetCore;
using Infrastructure;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Presentation.Middlewares;
using Presentation.Options;
using Presentation.Validators;

namespace Presentation.Extensions;

public static class ProgramExtension
{
    public static WebApplicationBuilder ConfigureBuilder
        (this WebApplicationBuilder builder)
    {
        builder.Services
            .AddDbContext(builder.Configuration)
            .AddRepositories()
            .AddServices()
            .AddTokenOptions(builder.Configuration)
            .AddIdentity(builder.Configuration)
            .AddOptions()
            .AddSwagger()
            .AddCors(options => options.ConfigureAllowAllCors())
            .AddControllers()
            .AddFluentValidation();
        
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

    private static IServiceCollection AddTokenOptions(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddOptionsValidators();
        
        return serviceCollection.Configure<TokenOptions>(options =>
        {
            configuration.GetSection(nameof(TokenOptions)).Bind(options);
        });
    }

    private static IServiceCollection AddOptionsValidators(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IValidateOptions<TokenOptions>, TokenOptionsValidator>();
        
        return serviceCollection;
    }
}