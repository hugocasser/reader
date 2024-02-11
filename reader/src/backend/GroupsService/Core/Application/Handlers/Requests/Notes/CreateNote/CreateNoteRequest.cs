using Application.Common;
using Application.Dtos.Views;
using MediatR;

namespace Application.Handlers.Requests.Notes.CreateNote;

public record CreateNoteRequest(Guid BookId, string Text, Guid GroupId, int NotePosition, Guid RequestingUserId )
    : IRequest<Result<NoteViewDto>>;