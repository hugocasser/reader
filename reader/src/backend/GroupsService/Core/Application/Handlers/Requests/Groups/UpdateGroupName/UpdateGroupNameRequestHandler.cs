using Application.Abstractions.Repositories;
using Application.Exceptions;
using MediatR;

namespace Application.Handlers.Requests.Groups.UpdateGroupName;

public class UpdateGroupNameRequestHandler(IGroupsRepository groupsRepository) : IRequestHandler<UpdateGroupNameRequest>
{
    public async Task Handle(UpdateGroupNameRequest request, CancellationToken cancellationToken)
    {
        var group = await groupsRepository.GetGroupByIdAsync(request.GroupId);

        if (group is null)
        {
            throw new NotFoundException("Group not found");
        }
        
        if (request.UserId != group.AdminId)
        {
            throw new BadRequestException("You are not the admin of this group");
        }
        
        group.GroupName = request.Name;
        
        await groupsRepository.UpdateGroupAsync(group);
        await groupsRepository.SaveChangesAsync();
    }
}