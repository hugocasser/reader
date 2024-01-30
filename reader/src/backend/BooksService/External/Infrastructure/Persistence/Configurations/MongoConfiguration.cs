namespace Infrastructure.Persistence.Configurations;

public class MongoConfiguration
{
    public string ConnectionUri { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public IEnumerable<string> CollectionsNames { get; set; } = null!;
    
}