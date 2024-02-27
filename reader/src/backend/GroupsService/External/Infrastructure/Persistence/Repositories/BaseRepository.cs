using System.Collections;
using Application.Abstractions.Repositories;
using Application.Dtos.Views;
using Domain.Abstractions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class BaseRepository<TEntity, TView>(WriteDbContext _writeDbContext, ReadDbContext _readDbContext)
    : IBaseRepository<TEntity, TView> where TEntity : Entity 
{
    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _writeDbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _writeDbContext.Set<TEntity>().Update(entity);

        return Task.CompletedTask;
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _writeDbContext.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
        _writeDbContext.Set<TEntity>().Remove(entity);
    }

    public async Task CreateAsyncInReadDbContextAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _readDbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsyncInReadDbContextAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _readDbContext.Set<TEntity>().Update(entity);

        return Task.CompletedTask;
    }

    public async Task DeleteByIdAsyncInReadDbContextAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _readDbContext.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
        _readDbContext.Set<TEntity>().Remove(entity);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _readDbContext.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public async Task<IList<TView>> GetAllAsync(PageSettingsRequestDto pageSettingsRequestDto, CancellationToken cancellationToken)
    {
        return await _readDbContext.Set<TEntity>().Skip(pageSettingsRequestDto.SkipCount())
            .Take(pageSettingsRequestDto.PageSize).ProjectToType<TView>().ToListAsync(cancellationToken);
    }

    public async Task<IList<TEntity>> GetByAsync(Func<TEntity, bool> func, CancellationToken cancellationToken)
    {
       return await _readDbContext.Set<TEntity>().Where(entity => func(entity) == true).ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _writeDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsExistByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _readDbContext.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken) != null;
    }
}