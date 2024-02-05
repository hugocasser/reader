using System.Reflection;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace Presentation.Extensions;

public static class LoggingExtension
{
    public static WebApplication UseLoggingDependOnEnvironment(this WebApplication application)
    {
        if (Environment.GetEnvironmentVariable("ElasticConfiguration:Uri") != "no_set")
        {
            application.UseSerilogRequestLogging();
        }
        
        return application;
    }
    
    public static WebApplicationBuilder AddLoggingServices(
        this WebApplicationBuilder builder)
    {
        if (Environment.GetEnvironmentVariable("ElasticConfiguration:Uri") != "no_set")
        {
            return builder.AddElasticAndSerilog();
        }
        
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        
        return builder;
    }
    
    private static WebApplicationBuilder AddElasticAndSerilog(
        this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
        {
            
            var configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            configuration
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithExceptionDetails()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticSink(configurationRoot))
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                .ReadFrom.Configuration(context.Configuration);
        });

        return builder;
    }
    
    private static ElasticsearchSinkOptions ConfigureElasticSink(IConfiguration configuration)
    {
        var connectionString = configuration["ElasticConfiguration:Uri"];
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new NullReferenceException("ElasticConfiguration:Uri configuration is empty");
        }
        
        var connectionUri = new Uri(connectionString);

        return new ElasticsearchSinkOptions(connectionUri) 
        {
            AutoRegisterTemplate = true,
            IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name?.ToLower().Replace('.', '-')}-{DateTime.UtcNow:yyyy-MM}",
            NumberOfReplicas = 1,
            NumberOfShards = 2,
        };
    }
}