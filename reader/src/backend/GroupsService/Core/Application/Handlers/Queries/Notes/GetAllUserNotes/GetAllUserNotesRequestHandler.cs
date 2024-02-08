using Application.Abstractions.Repositories;
using Application.Dtos.Views;
using Application.Exceptions;
using MediatR;

namespace Application.Handlers.Queries.Notes.GetAllUserNotes;

public class GetAllUserNotesRequestHandler(IUserBookProgressRepository userBookProgressRepository)
    : IRequestHandler<GetAllUserNotesRequest, IEnumerable<NoteViewDto>>
{
    public async Task<IEnumerable<NoteViewDto>> Handle(GetAllUserNotesRequest request, CancellationToken cancellationToken)
    {
        var userBookProgresses = await userBookProgressRepository.GetProgressesByUserIdAsync(request.UserId, request.PageSettings);

        if (!userBookProgresses.Any())
        {
            throw new NotFoundException("No notes found");
        }

        return (from userBookProgress in userBookProgresses
            from userNote in userBookProgress.UserNotes
            select NoteViewDto.MapFromModel(userNote, userBookProgress.User)).ToList();
    }
}