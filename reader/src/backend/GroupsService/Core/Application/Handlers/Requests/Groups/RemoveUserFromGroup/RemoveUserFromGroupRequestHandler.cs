using Application.Abstractions.Repositories;
using Application.Exceptions;
using MediatR;

namespace Application.Handlers.Requests.Groups.RemoveUserFromGroup;

public class RemoveUserFromGroupRequestHandler(IGroupsRepository groupsRepository) : IRequestHandler<RemoveUserFromGroupRequest>
{
    public async Task Handle(RemoveUserFromGroupRequest request, CancellationToken cancellationToken)
    {
        var group = await groupsRepository.GetGroupByIdAsync(request.GroupId);

        if (group is null)
        {
            throw new NotFoundException("Group not found");
        }
        
        var userToDelete = group.Members.FirstOrDefault(user => user.Id == request.UserToDeleteId);
        
        if ( userToDelete is null)
        {
            throw new NotFoundException("User not found in group");
        }

        if (request.DeleterId != group.AdminId)
        {
            throw new BadRequestException("You are not the admin of this group");
        }
        
        group.Members.Remove(userToDelete);
        
        await groupsRepository.UpdateGroupAsync(group);
        await groupsRepository.SaveChangesAsync();
    }
}