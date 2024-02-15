using Application.Common;
using MediatR;

namespace Application.Requests.Commands.Notes.DeleteNote;

public class DeleteNoteCommand
    : IRequest<Result<string>>, IRequestWithRequestingUserId
{
    public Guid? RequestingUserId { get; set; }
    public Guid NoteId { get; init; }
}