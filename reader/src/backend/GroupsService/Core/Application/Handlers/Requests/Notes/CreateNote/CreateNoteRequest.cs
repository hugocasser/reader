using MediatR;

namespace Application.Handlers.Requests.Notes.CreateNote;

public record CreateNoteRequest(Guid UserId, Guid BookId, string Text, Guid GroupId, int NotePosition ) : IRequest;