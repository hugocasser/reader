using Application.Abstractions.Repositories;
using Application.Common;
using Application.Dtos.Views;
using MapsterMapper;
using MediatR;

namespace Application.Handlers.Queries.Notes.GetAllGroupNotes;

public class GetAllGroupNotesRequestHandler(IGroupsRepository _groupsRepository, IMapper _mapper) : IRequestHandler<GetAllGroupNotesRequest, Result<IEnumerable<NoteViewDto>>>
{
    public async Task<Result<IEnumerable<NoteViewDto>>> Handle(GetAllGroupNotesRequest request, CancellationToken cancellationToken)
    {
        var group = await _groupsRepository.GetByIdAsync(request.GroupId, cancellationToken);

        if (group is null)
        {
            return new Result<IEnumerable<NoteViewDto>>(new Error("Group not found", 404));
        }
        
        if (group.Members.All(user => user.Id != request.RequestingUserId))
        {
            return new Result<IEnumerable<NoteViewDto>>(new Error("You are not a member of this group", 400));
        }
        
        var usersNotes = await _groupsRepository.GetGroupNotesAsync(request.GroupId, request.PageSettingsRequestDto);
        
        return new Result<IEnumerable<NoteViewDto>>(usersNotes.Select(_mapper.Map<NoteViewDto>));
    }
}