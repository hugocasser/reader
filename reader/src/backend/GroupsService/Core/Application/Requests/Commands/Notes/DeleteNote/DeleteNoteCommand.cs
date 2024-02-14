using Application.Common;
using MediatR;

namespace Application.Requests.Commands.Notes.DeleteNote;

public record DeleteNoteCommand(Guid NoteId, Guid RequestingUserId) : IRequest<Result<string>>;