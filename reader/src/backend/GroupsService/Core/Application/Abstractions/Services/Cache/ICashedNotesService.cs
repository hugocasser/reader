using Domain.Models;

namespace Application.Abstractions.Services.Cache;

public interface ICashedNotesService
{
    public Task CreateNoteAsync(Note note);
    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<Note?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<IEnumerable<Note?>> GetNotesAsync(int count);
    public Task RemoveRangeAsync(IEnumerable<Guid> keys);
}