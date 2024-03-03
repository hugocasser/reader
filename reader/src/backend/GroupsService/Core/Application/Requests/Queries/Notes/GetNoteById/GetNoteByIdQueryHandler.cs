using Application.Abstractions.Repositories;
using Application.Dtos.Views;
using Application.Results;
using Application.Results.Errors;
using MapsterMapper;
using MediatR;

namespace Application.Requests.Queries.Notes.GetNoteById;

public class GetNoteByIdQueryHandler(INotesRepository _notesRepository, IUsersRepository _usersRepository, IMapper _mapper) : IRequestHandler<GetNoteByIdQuery, Result<NoteViewDto>>
{
    public async Task<Result<NoteViewDto>> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
    {
        var note = await _notesRepository.GetByIdAsync(request.NoteId, cancellationToken);

        if (note is null)
        {
            return new Result<NoteViewDto>(new NotFoundError("Note"));
        }

        if (note.UserBookProgress.User.Id != request.RequestingUserId)
        {
            return new Result<NoteViewDto>(new BadRequestError("You are not owner of this note"));
        }
        
        var user = await _usersRepository.GetByIdAsync(request.RequestingUserId ??Guid.Empty, cancellationToken);
        
        return new Result<NoteViewDto>(_mapper.Map<NoteViewDto>(note));
    }
}