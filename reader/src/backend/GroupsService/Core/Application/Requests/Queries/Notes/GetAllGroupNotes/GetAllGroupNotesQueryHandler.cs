using Application.Abstractions.Repositories;
using Application.Common;
using Application.Dtos.Views;
using Application.Requests.Queries.Notes.GetAllGroupNotes;
using MapsterMapper;
using MediatR;

namespace Application.Handlers.Queries.Notes.GetAllGroupNotes;

public class GetAllGroupNotesQueryHandler(IGroupsRepository _groupsRepository, IMapper _mapper) : IRequestHandler<GetAllGroupNotesQuery, Result<IEnumerable<NoteViewDto>>>
{
    public async Task<Result<IEnumerable<NoteViewDto>>> Handle(GetAllGroupNotesQuery query, CancellationToken cancellationToken)
    {
        var group = await _groupsRepository.GetByIdAsync(query.GroupId, cancellationToken);

        if (group is null)
        {
            return new Result<IEnumerable<NoteViewDto>>(new Error("Group not found", 404));
        }
        
        if (group.Members.All(user => user.Id != query.RequestingUserId))
        {
            return new Result<IEnumerable<NoteViewDto>>(new Error("You are not a member of this group", 400));
        }
        
        var usersNotes = await _groupsRepository.GetGroupNotesAsync(query.GroupId, query.PageSettingsRequestDto, cancellationToken);
        
        return new Result<IEnumerable<NoteViewDto>>(usersNotes.Select(_mapper.Map<NoteViewDto>));
    }
}