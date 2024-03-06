using Application.Abstractions.Repositories;
using Application.Abstractions.Services.Cache;
using Application.Dtos.Views;
using Domain.Models;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Repositories;

public class CashedNotesRepository(INotesRepository _notesRepository, IRedisCacheService _redisCacheService) : INotesRepository
{
    public Task CreateAsync(Note entity, CancellationToken cancellationToken)
    {
        return _notesRepository.CreateAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(Note entity, CancellationToken cancellationToken)
    {
        return _notesRepository.UpdateAsync(entity, cancellationToken);
    }

    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _notesRepository.DeleteByIdAsync(id, cancellationToken);
    }

    public Task CreateAsyncInReadDbContextAsync(Note entity, CancellationToken cancellationToken)
    {
        return _notesRepository.CreateAsyncInReadDbContextAsync(entity, cancellationToken);
    }

    public Task UpdateAsyncInReadDbContextAsync(Note entity, CancellationToken cancellationToken)
    {
        return _notesRepository.UpdateAsyncInReadDbContextAsync(entity, cancellationToken);
    }

    public Task DeleteByIdAsyncInReadDbContextAsync(Guid id, CancellationToken cancellationToken)
    {
        return _notesRepository.DeleteByIdAsyncInReadDbContextAsync(id, cancellationToken);
    }

    public async Task<Note?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _redisCacheService.GetOrCreateAsync(CachingKeys.NoteById(id),
            async () => await _notesRepository.GetByIdAsync(id, cancellationToken));
    }

    public Task<IList<NoteViewDto>> GetAllAsync(PageSettingsRequestDto pageSettingsRequestDto,
        CancellationToken cancellationToken)
    {
        return _notesRepository.GetAllAsync(pageSettingsRequestDto, cancellationToken);
    }

    public Task<IList<Note>> GetByAsync(Func<Note, bool> func, CancellationToken cancellationToken)
    {
        return _notesRepository.GetByAsync(func, cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _notesRepository.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> IsExistByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _notesRepository.IsExistByIdAsync(id, cancellationToken);
    }
}