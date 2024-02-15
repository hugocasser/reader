namespace BusinessLogicLayer.Abstractions.Configurations;

public interface ITokenGenerationConfiguration
{
    public string Issuer { get; }
    public string Audience { get; }
    public string Key { get; }
}