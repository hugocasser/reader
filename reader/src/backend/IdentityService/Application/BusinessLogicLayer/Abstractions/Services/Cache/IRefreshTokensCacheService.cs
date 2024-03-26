using DataAccessLayer.Models;

namespace BusinessLogicLayer.Abstractions.Services.Cache;

public interface IRefreshTokensCacheService
{
    public Task SetAsync(RefreshToken token, CancellationToken cancellationToken);
    public Task<RefreshToken?> GetAsync(Guid id, CancellationToken cancellationToken);
    public Task RemoveAsync(Guid id, CancellationToken cancellationToken);
}