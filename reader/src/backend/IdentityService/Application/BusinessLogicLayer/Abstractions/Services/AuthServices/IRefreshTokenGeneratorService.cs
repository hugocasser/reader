using DataAccessLayer.Models;

namespace BusinessLogicLayer.Abstractions.Services.AuthServices;

public interface IRefreshTokenGeneratorService
{
    public RefreshToken GenerateToken(Guid userId);
    public Task<RefreshToken> ValidateTokenAsync(Guid userId, string token, CancellationToken cancellationToken);
}