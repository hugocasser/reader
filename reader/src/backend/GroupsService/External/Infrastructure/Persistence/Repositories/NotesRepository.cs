using Application.Abstractions.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class NotesRepository(WriteDbContext _writeDbContext, ReadDbContext _readDbContext)
    : BaseRepository<Note>(_writeDbContext, _readDbContext), INotesRepository
{
    public Task<List<Note>> GetNotesByGroupIdAndBookIdAsync(Guid groupId, Guid bookId,
        CancellationToken cancellationToken)
    {
        return Queryable.Order(_readDbContext.Notes.Where(note =>
                note.UserBookProgress.Book.Id == bookId && note.UserBookProgress.Group.Id == groupId)).ToListAsync(cancellationToken: cancellationToken);
    }
}