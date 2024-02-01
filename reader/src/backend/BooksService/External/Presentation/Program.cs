using Presentation.Configurations;
using Presentation.Extensions;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication
            .CreateBuilder(args);
        var mongoConfiguration = new MongoConfiguration(builder.Configuration);
        builder.ConfigureBuilder(mongoConfiguration);
        
        var application = builder
            .Build()
            .ConfigureApplication();
        
        await application.RunAsync();
    }
}
