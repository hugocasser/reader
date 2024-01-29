namespace DataAccessLayer.Abstractions.Configurations;

public interface IApplicationConfiguration
{
    public string DatabaseConnectionString { get; }
    public bool IsDevelopmentEnvironment { get; set; }
}