using Application.Dtos.Views;
using Domain.Abstractions;

namespace Application.Abstractions.Repositories;

public interface IBaseRepository<T> where T : Entity
{
    public Task CreateAsync(T entity, CancellationToken cancellationToken);
    public Task UpdateAsync(T entity, CancellationToken cancellationToken);
    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<IEnumerable<T>> GetAllAsync(PageSettingsRequestDto pageSettingsRequestDto, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
    
    public Task<bool> IsExistByIdAsync(Guid id, CancellationToken cancellationToken);
}