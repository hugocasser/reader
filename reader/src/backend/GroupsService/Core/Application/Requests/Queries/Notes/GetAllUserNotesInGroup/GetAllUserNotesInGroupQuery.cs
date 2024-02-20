using Application.Common;
using Application.Dtos.Views;
using MediatR;

namespace Application.Requests.Queries.Notes.GetAllUserNotesInGroup;

public record GetAllUserNotesInGroupQuery
    (Guid UserId,
        Guid GroupId,
        PageSettingsRequestDto PageSettingsRequestDto)
    : IRequest<Result<IEnumerable<NoteViewDto>>>, IRequestWithRequestingUserId
{
    public Guid? RequestingUserId { get; set; }
}