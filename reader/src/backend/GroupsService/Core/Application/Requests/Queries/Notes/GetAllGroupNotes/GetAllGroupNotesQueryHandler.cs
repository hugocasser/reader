using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Common;
using Application.Dtos.Views;
using Application.Results;
using Application.Results.Errors;
using MapsterMapper;
using MediatR;

namespace Application.Requests.Queries.Notes.GetAllGroupNotes;

public class GetAllGroupNotesQueryHandler(IGroupsRepository _groupsRepository, IMapper _mapper) : IRequestHandler<GetAllGroupNotesQuery, Result<IEnumerable<NoteViewDto>>>
{
    public async Task<Result<IEnumerable<NoteViewDto>>> Handle(GetAllGroupNotesQuery query, CancellationToken cancellationToken)
    {
        var group = await _groupsRepository.GetByIdAsync(query.GroupId, cancellationToken);

        if (group is null)
        {
            return new Result<IEnumerable<NoteViewDto>>(new NotFoundError("Group"));
        }
        
        if (group.Members.All(user => user.Id != query.RequestingUserId))
        {
            return new Result<IEnumerable<NoteViewDto>>(new BadRequestError("You aren't member of this group"));
        }
        
        var usersNotes = await _groupsRepository.GetGroupNotesAsync(query.GroupId, query.PageSettingsRequestDto, cancellationToken);
        
        return new Result<IEnumerable<NoteViewDto>>(usersNotes.Select(_mapper.Map<NoteViewDto>));
    }
}