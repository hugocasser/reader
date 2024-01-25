namespace BusinessLogicLayer.Abstractions.Services;

public interface IAuthTokenGeneratorService
{
    public string GenerateToken(Guid userId, string userEmail, string role);
}