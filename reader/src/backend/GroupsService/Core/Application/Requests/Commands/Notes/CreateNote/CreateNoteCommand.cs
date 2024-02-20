using Application.Common;
using Application.Dtos.Views;
using MediatR;

namespace Application.Requests.Commands.Notes.CreateNote;

public class CreateNoteCommand
    : IRequest<Result<NoteViewDto>> , IRequestWithRequestingUserId
{
    public Guid? RequestingUserId { get; set; }
    public Guid BookId { get; init; }
    public string Text { get; init; }
    public Guid GroupId { get; init; }
    public int NotePosition { get; init; }
}