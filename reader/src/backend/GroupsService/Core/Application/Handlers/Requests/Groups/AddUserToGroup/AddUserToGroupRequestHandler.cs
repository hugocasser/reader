using Application.Abstractions.Repositories;
using Application.Exceptions;
using MediatR;

namespace Application.Handlers.Requests.Groups.AddUserToGroup;

public class AddUserToGroupRequestHandler(IGroupsRepository groupsRepository, IUsersRepository usersRepository) : IRequestHandler<AddUserToGroupRequest>
{
    public async Task Handle(AddUserToGroupRequest request, CancellationToken cancellationToken)
    {
        var invitedUser = await usersRepository.GetUserByIdAsync(request.InvitedUser);

        if (invitedUser is null)
        {
            throw new NotFoundException("User not found");
        }
        
        var group = await groupsRepository.GetGroupByIdAsync(request.GroupId);

        if (group is null)
        {
            throw new NotFoundException("Group not found");
        }

        if (request.InviterId != group.Members.FirstOrDefault(user => user.Id == request.InviterId)?.Id)
        {
            throw new BadRequestException("You are not a member of this group");
        }

        if (group.Members.Any(user => user.Id == request.InvitedUser))
        {
            throw new BadRequestException("User already in group");
        }
        
        group.Members.Add(invitedUser);
        
        await groupsRepository.UpdateGroupAsync(group);
        await groupsRepository.SaveChangesAsync();
    }
}