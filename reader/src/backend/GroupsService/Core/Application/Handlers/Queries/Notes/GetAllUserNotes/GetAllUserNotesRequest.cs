using Application.Dtos.Views;
using MediatR;

namespace Application.Handlers.Queries.Notes.GetAllUserNotes;

public record GetAllUserNotesRequest(Guid UserId, PageSettings PageSettings) : IRequest<IEnumerable<NoteViewDto>>;