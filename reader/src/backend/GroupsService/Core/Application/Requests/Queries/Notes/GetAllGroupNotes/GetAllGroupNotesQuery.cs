using Application.Abstractions;
using Application.Common;
using Application.Dtos.Requests;
using Application.Dtos.Views;
using Application.Results;
using MediatR;

namespace Application.Requests.Queries.Notes.GetAllGroupNotes;

public class GetAllGroupNotesQuery
    : IRequest<Result<IEnumerable<NoteViewDto>>> , IRequestWithRequestingUserId
{
    public Guid? RequestingUserId { get; set; }
    public Guid GroupId { get; init; }
    public ReadingPageSettingsRequestDto PageSettingsRequestDto { get; init; }
}