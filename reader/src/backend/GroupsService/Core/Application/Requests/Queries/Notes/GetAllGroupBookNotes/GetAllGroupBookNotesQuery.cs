using Application.Common;
using Application.Dtos.Requests;
using Application.Dtos.Views;
using MediatR;

namespace Application.Requests.Queries.Notes.GetAllGroupBookNotes;

public record GetAllGroupBookNotesQuery(Guid GroupId,Guid BookId, ReadingPageSettingsRequestDto PageSettingsRequestDto)
    : IRequest<Result<IEnumerable<NoteViewDto>>>, IRequestWithRequestingUserId
{
    public Guid? RequestingUserId { get; set; }
}