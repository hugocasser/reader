using Application.Abstractions;
using Application.Common;
using Application.Dtos.Views;
using Application.Results;
using MediatR;

namespace Application.Requests.Queries.Notes.GetAllUserNotes;

public record GetAllUserNotesQuery
    : IRequest<Result<IEnumerable<NoteViewDto>>>, IRequestWithRequestingUserId
{
    public Guid? RequestingUserId { get; set; }
    public PageSettingsRequestDto PageSettingsRequestDto { get; init; }
}