using Application.Common;
using Application.Dtos.Views;
using MediatR;

namespace Application.Handlers.Queries.Notes.GetAllUserNotes;

public record GetAllUserNotesQuery(Guid RequestingUserId, PageSettingsRequestDto PageSettingsRequestDto) : IRequest<Result<IEnumerable<NoteViewDto>>>;