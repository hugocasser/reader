using DataAccessLayer.Abstractions.Configurations;

namespace PresentationLayer.Configurations;

public class ApplicationConfiguration : IApplicationConfiguration
{
    public string DatabaseConnectionString { get; private set; }
    
    public ApplicationConfiguration(IConfiguration configuration)
    {
        
        _ = SetupDbConnectionString(configuration) ? true : throw new Exception("Database connection string not set");
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