using Application.Dtos.Requests;
using Domain.Abstractions;

namespace Application.Abstractions.Repositories;

public interface IBaseRepository<T> where T : Entity
{
    public Task UpdateAsync(T entity, CancellationToken cancellationToken);
    public Task AddAsync(T entity, CancellationToken cancellationToken);
    public Task<IEnumerable<T>> GetAllAsync(PageSettingRequestDto pageSettingRequestDto, CancellationToken cancellationToken);
    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<Entity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<bool> IsExistsAsync(Guid id, CancellationToken cancellationToken);
}