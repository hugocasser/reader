using Application.Common;
using Application.Dtos.Views;
using MediatR;

namespace Application.Handlers.Queries.Notes.GetNoteById;

public record GetNoteByIdQuery(Guid NoteId, Guid RequestingUserId) : IRequest<Result<NoteViewDto>>;