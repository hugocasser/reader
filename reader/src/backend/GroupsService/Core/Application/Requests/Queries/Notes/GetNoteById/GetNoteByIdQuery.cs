using Application.Common;
using Application.Dtos.Views;
using MediatR;

namespace Application.Requests.Queries.Notes.GetNoteById;

public class GetNoteByIdQuery : IRequest<Result<NoteViewDto>>, IRequestWithRequestingUserId
{
    public Guid? RequestingUserId { get; set; }
    public Guid NoteId { get; init; }
}