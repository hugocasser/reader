using DataAccessLayer.Models;

namespace DataAccessLayer.Abstractions.Repositories;

public interface IRefreshTokensRepository
{
    public Task<IEnumerable<RefreshToken>> GetAllAsync (CancellationToken cancellationToken);
    public Task AddAsync(RefreshToken token, CancellationToken cancellationToken);
    public Task<RefreshToken> FindUserTokenAsync(Guid userId, string token, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}