using Application.Common;
using MediatR;

namespace Application.Handlers.Requests.Notes.DeleteNote;

public record DeleteNoteRequest(Guid NoteId, Guid RequestingUserId) : IRequest<Result<string>>;