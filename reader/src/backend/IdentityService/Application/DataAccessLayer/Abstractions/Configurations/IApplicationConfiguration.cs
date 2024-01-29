namespace DataAccessLayer.Abstractions.Configurations;

public interface IApplicationConfiguration
{
    public string DatabaseConnectionString { get; }
}