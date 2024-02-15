using BusinessLogicLayer.Abstractions.Configurations;

namespace PresentationLayer.Configurations;

public class EmailMessageSenderConfiguration : IEmailMessageSenderConfiguration
{
    public string Sender { get; set; }
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ConfirmUrl { get; set; }

    public EmailMessageSenderConfiguration(IConfiguration configuration)
    {
        Sender = configuration.GetConfigurationStringOrThrowException("Smtp:Sender");
        SmtpServer = configuration.GetConfigurationStringOrThrowException("Smtp:Server");
        Port = int.Parse(configuration.GetConfigurationStringOrThrowException("Smtp:Port"));
        UserName = configuration.GetConfigurationStringOrThrowException("Smtp:Username");
        Password = configuration.GetConfigurationStringOrThrowException("Smtp:Password");
        ConfirmUrl = configuration.GetConfigurationStringOrThrowException("Smtp:ConfirmUrl");
    }
}

file static class ConfigurationExtension
{
    public static string GetConfigurationStringOrThrowException(
        this IConfiguration configuration,
        string configurationString)
    {
        return configuration[configurationString] 
               ?? throw new InvalidOperationException($"The {configurationString} is empty. Please set the {configurationString} in appsettings.json");
    }
}