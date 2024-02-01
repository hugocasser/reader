namespace Infrastructure.Abstractions;

public interface IMongoConfiguration
{
    public string ConnectionUri { get; set; }
    public string DatabaseName { get; set; }
    public IEnumerable<string> CollectionsNames { get; set; }
}