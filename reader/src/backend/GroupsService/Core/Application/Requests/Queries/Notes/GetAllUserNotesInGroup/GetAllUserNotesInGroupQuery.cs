using Application.Common;
using Application.Dtos.Views;
using MediatR;

namespace Application.Handlers.Queries.Notes.GetAllUserNotes;

public record GetAllUserNotesInGroupQuery(Guid UserId, Guid GroupId,
    PageSettingsRequestDto PageSettingsRequestDto, Guid RequestingUserId)
    : IRequest<Result<IEnumerable<NoteViewDto>>>;