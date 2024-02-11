using Application.Abstractions.Repositories;
using Application.Common;
using MediatR;

namespace Application.Handlers.Requests.Groups.RemoveUserFromGroup;

public class RemoveUserFromGroupRequestHandler(IGroupsRepository groupsRepository)
    : IRequestHandler<RemoveUserFromGroupRequest, Result<string>>
{
    public async Task<Result<string>> Handle(RemoveUserFromGroupRequest request, CancellationToken cancellationToken)
    {
        var group = await groupsRepository.GetByIdAsync(request.GroupId, cancellationToken);

        if (group is null)
        {
            return new Result<string>(new Error("Group not found", 404));
        }
        
        var userToDelete = group.Members.FirstOrDefault(user => user.Id == request.UserToRemoveId);
        
        if (userToDelete is null)
        {
            return new Result<string>(new Error("User not member of this group", 404));
        }

        if (request.RequestingUserId != group.AdminId || userToDelete.Id != request.RequestingUserId)
        {
            return new Result<string>(new Error("You can't remove user from this group", 400));
        }
        
        group.Members.Remove(userToDelete);
        
        await groupsRepository.UpdateAsync(group, cancellationToken);
        await groupsRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<string>("User removed from group");
    }
}