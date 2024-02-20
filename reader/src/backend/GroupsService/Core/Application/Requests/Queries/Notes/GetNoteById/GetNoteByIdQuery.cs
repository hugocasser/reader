using Application.Common;
using Application.Dtos.Views;
using MediatR;

namespace Application.Requests.Queries.Notes.GetNoteById;

public record GetNoteByIdQuery(Guid NoteId) : IRequest<Result<NoteViewDto>>, IRequestWithRequestingUserId
{
    public Guid? RequestingUserId { get; set; }
}