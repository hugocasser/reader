using Application.Abstractions.Repositories;
using Application.Common;
using MediatR;

namespace Application.Requests.Commands.Groups.RemoveUserFromGroup;

public class RemoveUserFromGroupCommandHandler(IGroupsRepository groupsRepository)
    : IRequestHandler<RemoveUserFromGroupCommand, Result<string>>
{
    public async Task<Result<string>> Handle(RemoveUserFromGroupCommand command, CancellationToken cancellationToken)
    {
        var group = await groupsRepository.GetByIdAsync(command.GroupId, cancellationToken);

        if (group is null)
        {
            return new Result<string>(new Error("Group not found", 404));
        }
        
        var userToDelete = group.Members.FirstOrDefault(user => user.Id == command.UserToRemoveId);
        
        if (userToDelete is null)
        {
            return new Result<string>(new Error("User not member of this group", 404));
        }

        if (command.RequestingUserId != group.AdminId || userToDelete.Id != command.RequestingUserId && group.AdminId != userToDelete.Id)
        {
            return new Result<string>(new Error("You can't remove user from this group", 400));
        }
        
        group.RemoveMember(userToDelete);
        
        await groupsRepository.UpdateAsync(group, cancellationToken);
        await groupsRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<string>("User removed from group");
    }
}