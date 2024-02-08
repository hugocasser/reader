using Application.Dtos;
using Application.Dtos.Views;
using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface INotesRepository
{
    public Task CreateNoteAsync(Note note);
    public Task<Note?> GetNoteAsync(Guid id);
    public Task<IEnumerable<Note>> GetNotesAsync(PageSettings pageSettings);
    public Task DeleteNoteByIdAsync(Guid noteId);
    public Task SaveChangesAsync();
}