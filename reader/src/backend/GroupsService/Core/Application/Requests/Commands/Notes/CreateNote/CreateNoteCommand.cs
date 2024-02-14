using Application.Common;
using Application.Dtos.Views;
using MediatR;

namespace Application.Requests.Commands.Notes.CreateNote;

public record CreateNoteCommand(Guid BookId, string Text, Guid GroupId, int NotePosition, Guid RequestingUserId )
    : IRequest<Result<NoteViewDto>>;