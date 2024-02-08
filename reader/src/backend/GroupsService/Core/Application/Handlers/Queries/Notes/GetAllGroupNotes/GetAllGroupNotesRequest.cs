using Application.Dtos.Views;
using Domain.Models;
using MediatR;

namespace Application.Handlers.Queries.Notes.GetAllGroupNotes;

public record GetAllGroupNotesRequest(Guid GroupId, Guid UserId, ReadingPageSettings PageSettings): IRequest<IEnumerable<NoteViewDto>>;