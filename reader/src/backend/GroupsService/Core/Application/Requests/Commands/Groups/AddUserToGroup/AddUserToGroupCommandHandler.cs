using Application.Abstractions.Repositories;
using Application.Common;
using MediatR;

namespace Application.Requests.Commands.Groups.AddUserToGroup;

public class AddUserToGroupCommandHandler(IGroupsRepository groupsRepository,
    IUsersRepository usersRepository)
    : IRequestHandler<AddUserToGroupCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AddUserToGroupCommand command, CancellationToken cancellationToken)
    {
        var invitedUser = await usersRepository.GetByIdAsync(command.InvitedUser, cancellationToken);

        if (invitedUser is null)
        {
            return new Result<string>(new Error("User not found", 404));
        }
        
        var group = await groupsRepository.GetByIdAsync(command.GroupId, cancellationToken);

        if (group is null)
        {
            return new Result<string>(new Error("Group not found", 404));
        }

        if (command.RequestingUserId!= group.Members.FirstOrDefault(user => user.Id == command.RequestingUserId)?.Id)
        {
            return new Result<string>(new Error("You can't invite user to this group", 400));
        }

        if (group.Members.Any(user => user.Id == command.InvitedUser))
        {
            return new Result<string>(new Error("User is already a member of this group", 400));
        }
        
        group.AddMember(invitedUser);
        
        await groupsRepository.UpdateAsync(group, cancellationToken);
        await groupsRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<string>("User added to group");
    }
}