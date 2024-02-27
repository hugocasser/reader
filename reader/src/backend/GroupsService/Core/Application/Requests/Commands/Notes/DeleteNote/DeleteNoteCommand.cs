using Application.Abstractions;
using Application.Common;
using Application.Results;
using MediatR;

namespace Application.Requests.Commands.Notes.DeleteNote;

public class DeleteNoteCommand
    : IRequest<Result<string>>, IRequestWithRequestingUserId
{
    public Guid? RequestingUserId { get; set; }
    public Guid NoteId { get; init; }
    public Guid GroupId { get; init; }
}