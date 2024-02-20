using Application.Dtos.Requests;
using Application.Dtos.Views;
using Application.Requests.Commands.Books.EndReadSession;
using Application.Requests.Commands.Notes.CreateNote;
using Application.Requests.Commands.Notes.DeleteNote;

namespace Application.Abstractions.Hubs;

public interface INotesHub
{
    public Task SendNoteAsync(CreateNoteCommand command);
    public Task DeleteNoteAsync(DeleteNoteCommand command);
    public Task SendUserProgressAsync(EndReadingSessionCommand command);
}