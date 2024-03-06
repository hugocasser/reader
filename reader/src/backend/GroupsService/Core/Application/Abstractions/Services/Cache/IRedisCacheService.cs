using Domain.Abstractions;
using Domain.Models;

namespace Application.Abstractions.Services.Cache;

public interface IRedisCacheService
{
    public Task CreateAsync(Entity entity);
    public Task<bool> RemoveAsync(Guid key);
    public Task RemoveRangeAsync(IEnumerable<Guid> keys);
    public Task<Note?> GetByIdAsync(Guid key);
    public Task<IEnumerable<Note?>> GetRangeAsync(int count);
}