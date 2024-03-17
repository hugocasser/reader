using Presentation.Extensions;

namespace Presentation;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.ConfigureBuilder();
        
        var app = builder.Build()
            .ConfigureApplication();

        await app.RunApplicationAsync();
    }
}