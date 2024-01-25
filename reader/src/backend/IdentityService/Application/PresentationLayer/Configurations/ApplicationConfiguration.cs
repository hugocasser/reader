using DataAccessLayer.Abstractions.Configurations;

namespace PresentationLayer.Configurations;

public class ApplicationConfiguration : IApplicationConfiguration
{
    public string DatabaseConnectionString { get; private set; }
    public IApplicationConfiguration.Ports OpenPorts { get; }
    
    public ApplicationConfiguration(IConfiguration configuration)
    {
        
        _ = SetupDbConnectionString(configuration) ? true : throw new Exception("Database connection string not set");
        OpenPorts = new IApplicationConfiguration.Ports()
        {
            HttpsPort = 3001,
            HttpPort = 3000,
        };
    }
    
    private bool SetupDbConnectionString(IConfiguration configuration)
    {
        var dbConnectionString = configuration.GetConnectionString("IdentityDbContext");

        if (string.IsNullOrEmpty(dbConnectionString))
        {
            return false;
        }

        DatabaseConnectionString =  dbConnectionString;
        return true;
    }
}