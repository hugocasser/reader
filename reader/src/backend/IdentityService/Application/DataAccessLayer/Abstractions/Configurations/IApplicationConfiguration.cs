namespace DataAccessLayer.Abstractions.Configurations;

public interface IApplicationConfiguration
{
    public string DatabaseConnectionString { get; }
    public Ports OpenPorts { get; }
    
    public class Ports
    {
        public int HttpsPort { get; set; }
        public int HttpPort { get; set; }
    }
}