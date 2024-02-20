using Application.Abstractions.Repositories;
using Application.Common;
using Application.Dtos.Views;
using MapsterMapper;
using MediatR;

namespace Application.Requests.Queries.Notes.GetAllUserNotes;

public class GetAllUserNotesQueryHandler
    (IUsersRepository _usersRepository,
        IMapper _mapper) 
    : IRequestHandler<GetAllUserNotesQuery, Result<IEnumerable<NoteViewDto>>>
{
    public async Task<Result<IEnumerable<NoteViewDto>>> Handle(GetAllUserNotesQuery request, CancellationToken cancellationToken)
    {
        var user =await _usersRepository.GetByIdAsync(request.RequestingUserId ?? Guid.Empty, cancellationToken);

        if (user is null)
        {
            return new Result<IEnumerable<NoteViewDto>>(new Error("User not found", 404));
        }

        var userNotes = new List<NoteViewDto>();

        foreach (var userBookProgress in user.UserBookProgresses)
        {
            userNotes.AddRange(userBookProgress.UserNotes.Select(_mapper.Map<NoteViewDto>));
        }
        
        return new Result<IEnumerable<NoteViewDto>>(userNotes);
    }
}