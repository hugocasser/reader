using Application.Dtos;
using Application.Dtos.Views;
using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface INotesRepository : IBaseRepository<Note, NoteViewDto>
{
    public Task<List<Note>> GetNotesByGroupIdAndBookIdAsync(Guid groupId, Guid bookId,
        CancellationToken cancellationToken);
}