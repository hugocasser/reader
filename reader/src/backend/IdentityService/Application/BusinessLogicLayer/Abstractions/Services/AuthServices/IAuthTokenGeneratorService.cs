namespace BusinessLogicLayer.Abstractions.Services.AuthServices;

public interface IAuthTokenGeneratorService
{
    public string GenerateToken(Guid userId, string userEmail, IEnumerable<string> roles);
}