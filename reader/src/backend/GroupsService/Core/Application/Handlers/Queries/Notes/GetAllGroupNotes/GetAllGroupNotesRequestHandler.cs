using Application.Abstractions.Repositories;
using Application.Dtos.Views;
using Application.Exceptions;
using MediatR;

namespace Application.Handlers.Queries.Notes.GetAllGroupNotes;

public class GetAllGroupNotesRequestHandler(IGroupsRepository groupsRepository) : IRequestHandler<GetAllGroupNotesRequest, IEnumerable<NoteViewDto>>
{
    public async Task<IEnumerable<NoteViewDto>> Handle(GetAllGroupNotesRequest request, CancellationToken cancellationToken)
    {
        var group = await groupsRepository.GetGroupByIdAsync(request.GroupId);

        if (group is null)
        {
            throw new NotFoundException("Group not found");    
        }

        if (group.Members.All(user => user.Id == request.UserId))
        {
            throw new BadRequestException("You are not a member of this group");
        }
        
        var usersNotes = await groupsRepository.GetGroupNotesAsync(request.GroupId, request.PageSettings);
        
        return usersNotes.Select(userNote =>
            NoteViewDto.MapFromModel(userNote.Item1, userNote.Item2));
    }
}