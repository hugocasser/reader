using Application.Common;
using Application.Dtos.Views;
using MediatR;

namespace Application.Requests.Queries.Notes.GetAllUserNotesInGroup;

public class GetAllUserNotesInGroupQuery
    : IRequest<Result<IEnumerable<NoteViewDto>>>, IRequestWithRequestingUserId
{
    public Guid? RequestingUserId { get; set; }
    public PageSettingsRequestDto PageSettingsRequestDto { get; init; }
    public Guid UserId { get; init; }
    public Guid GroupId { get; init; }
}