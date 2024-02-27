using Application.Abstractions.Repositories;
using Application.Dtos.Requests;
using Domain.Abstractions;
using MongoDB.Driver;

namespace Infrastructure.Abstractions;

public abstract class BaseRepository<T>(IMongoCollection<T> _entities) : IBaseRepository<T> where T : Entity
{
    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        await _entities.ReplaceOneAsync(oldEntity => oldEntity.Id == entity.Id, entity, cancellationToken: cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _entities.InsertOneAsync(entity, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(PageSettingRequestDto pageSettingRequestDto, CancellationToken cancellationToken)
    {
        return await _entities.Find(_ => true)
            .Skip(pageSettingRequestDto.Skip())
            .Limit(pageSettingRequestDto.PageSize).ToListAsync(cancellationToken);
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _entities.DeleteOneAsync(entity => entity.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<Entity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _entities.Find(entity => entity.Id == id).FirstOrDefaultAsync(cancellationToken); 
    }

    public async Task<bool> IsExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _entities.Find(entity => entity.Id == id).AnyAsync(cancellationToken);
    }
}