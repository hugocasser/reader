using Application.Abstractions.Repositories;
using Application.Abstractions.Services.Cache;
using Domain.Models;

namespace Application.Services;

public class CachedNotesService(INotesRepository _notesRepository, IRedisCacheService _redisCacheService) : ICashedNotesService
{
    public async Task CreateNoteAsync(Note note)
    {
        await _redisCacheService.CreateAsync(note);
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _redisCacheService.RemoveAsync(id);
        if (!result)
        {
            await _notesRepository.DeleteByIdAsync(id, cancellationToken);
            await _notesRepository.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<Note?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var cashedNote = await _redisCacheService.GetByIdAsync(id);
        
        if (cashedNote is null)
        {
            return  await _notesRepository.GetByIdAsync(id, cancellationToken);
        }
        
        return cashedNote;
    }

    public async Task<IEnumerable<Note?>> GetNotesAsync(int count)
    {
        return await _redisCacheService.GetRangeAsync(count);
    }

    public async Task RemoveRangeAsync(IEnumerable<Guid> keys)
    {
        await _redisCacheService.RemoveRangeAsync(keys);
    }
}