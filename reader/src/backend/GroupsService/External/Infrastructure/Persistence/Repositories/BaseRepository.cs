using Application.Abstractions.Repositories;
using Application.Dtos.Views;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class BaseRepository<T>(WriteDbContext _writeDbContext, ReadDbContext _readDbContext)
    : IBaseRepository<T> where T : Entity
{
    public async Task CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await _writeDbContext.Set<T>().AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _writeDbContext.Set<T>().Update(entity);

        return Task.CompletedTask;
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _writeDbContext.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
        _writeDbContext.Set<T>().Remove(entity);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _readDbContext.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(PageSettingsRequestDto pageSettingsRequestDto, CancellationToken cancellationToken)
    {
        return await _readDbContext.Set<T>().Order().Skip(pageSettingsRequestDto.SkipCount())
            .Take(pageSettingsRequestDto.PageSize).ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _writeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsExistByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _readDbContext.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken) != null;
    }
}