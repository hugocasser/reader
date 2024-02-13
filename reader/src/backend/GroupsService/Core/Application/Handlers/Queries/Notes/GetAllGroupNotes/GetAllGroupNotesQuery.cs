using Application.Common;
using Application.Dtos.Requests;
using Application.Dtos.Views;
using MediatR;

namespace Application.Handlers.Queries.Notes.GetAllGroupNotes;

public record GetAllGroupNotesQuery(Guid GroupId, Guid RequestingUserId, ReadingPageSettingsRequestDto PageSettingsRequestDto)
    : IRequest<Result<IEnumerable<NoteViewDto>>>;