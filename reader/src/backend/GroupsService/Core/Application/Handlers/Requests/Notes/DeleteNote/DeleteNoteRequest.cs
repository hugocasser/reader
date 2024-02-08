using MediatR;

namespace Application.Handlers.Requests.Notes.DeleteNote;

public record DeleteNoteRequest(Guid NoteId, Guid UserId) : IRequest;