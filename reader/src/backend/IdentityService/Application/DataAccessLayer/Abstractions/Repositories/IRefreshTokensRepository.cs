using DataAccessLayer.Models;

namespace DataAccessLayer.Abstractions.Repositories;

public interface IRefreshTokensRepository
{
    public Task<IEnumerable<RefreshToken>> GetAllAsync(int skip, int pageSize, CancellationToken cancellationToken);
    public Task AddAsync(RefreshToken token, CancellationToken cancellationToken);
    public Task<RefreshToken> FindUserTokenAsync(Guid userId, string token, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
    public void RemoveToken(RefreshToken refreshToken, CancellationToken cancellationToken);
}