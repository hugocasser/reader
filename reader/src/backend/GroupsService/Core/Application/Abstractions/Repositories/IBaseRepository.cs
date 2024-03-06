using Application.Dtos.Views;
using Domain.Abstractions;

namespace Application.Abstractions.Repositories;

public interface IBaseRepository<TEntity, TView> where TEntity : Entity
{
    public Task CreateAsync(TEntity entity,  CancellationToken cancellationToken);
    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task CreateAsyncInReadDbContextAsync(TEntity entity,  CancellationToken cancellationToken);
    public Task UpdateAsyncInReadDbContextAsync(TEntity entity, CancellationToken cancellationToken);
    public Task DeleteByIdAsyncInReadDbContextAsync(Guid id, CancellationToken cancellationToken);
    public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<IList<TView>> GetAllAsync(PageSettingsRequestDto pageSettingsRequestDto,
        CancellationToken cancellationToken);
    public Task<IList<TEntity>> GetByAsync(Func<TEntity, bool> func, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
    public Task<bool> IsExistByIdAsync(Guid id, CancellationToken cancellationToken);
}