using Application.Common;
using Application.Dtos.Views;
using MediatR;

namespace Application.Requests.Queries.Notes.GetAllUserNotes;

public class GetAllUserNotesQuery
    : IRequest<Result<IEnumerable<NoteViewDto>>>, IRequestWithRequestingUserId
{
    public Guid? RequestingUserId { get; set; }
    public PageSettingsRequestDto PageSettingsRequestDto { get; init; }
}