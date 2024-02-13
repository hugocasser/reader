using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Common;
using MediatR;

namespace Application.Handlers.Requests.Groups.AddUserToGroup;

public class AddUserToGroupRequestHandler(IGroupsRepository groupsRepository, IDbSyncerService _dbSyncerService,
    IUsersRepository usersRepository)
    : IRequestHandler<AddUserToGroupRequest, Result<string>>
{
    public async Task<Result<string>> Handle(AddUserToGroupRequest request, CancellationToken cancellationToken)
    {
        var invitedUser = await usersRepository.GetByIdAsync(request.InvitedUser, cancellationToken);

        if (invitedUser is null)
        {
            return new Result<string>(new Error("User not found", 404));
        }
        
        var group = await groupsRepository.GetByIdAsync(request.GroupId, cancellationToken);

        if (group is null)
        {
            return new Result<string>(new Error("Group not found", 404));
        }

        if (request.RequestingUserId!= group.Members.FirstOrDefault(user => user.Id == request.RequestingUserId)?.Id)
        {
            return new Result<string>(new Error("You can't invite user to this group", 400));
        }

        if (group.Members.Any(user => user.Id == request.InvitedUser))
        {
            return new Result<string>(new Error("User is already a member of this group", 400));
        }
        
        group.Members.Add(invitedUser);
        
        await groupsRepository.UpdateAsync(group, cancellationToken);
        await _dbSyncerService.SendEventAsync(EventType.Updated, group, cancellationToken);
        await groupsRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<string>("User added to group");
    }
}