using Infrastructure.Abstractions;

namespace Presentation.Configurations;

public class MongoConfiguration : IMongoConfiguration
{
    public string ConnectionUri { get; set; }
    public string DatabaseName { get; set; }
    public IEnumerable<string> CollectionsNames { get; set; }

    public MongoConfiguration(IConfiguration configuration)
    {
        SetUpDbConnection(configuration);
    }

    private void SetUpDbConnection(IConfiguration configuration)
    {
        var connectionUri = configuration.GetValue<string?>("MongoConnectionString:ConnectionURI");
        var databaseName = configuration.GetValue<string?>("MongoConnectionString:DatabaseName");
        var collectionNames = configuration
            .GetSection("MongoConnectionString:CollectionNames")
            .Get<IEnumerable<string>>();
            
        if (connectionUri is null || databaseName is null || collectionNames is null)
        {
            throw new ArgumentNullException("MongoConnectionString");
        }
        
        
        ConnectionUri = connectionUri;
        DatabaseName = databaseName;
        CollectionsNames = collectionNames;
    }
}