using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Common;
using Application.Results;
using Application.Results.Errors;
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
            return new Result<string>(new NotFoundError("Group"));
        }
        
        var userToDelete = group.Members.FirstOrDefault(user => user.Id == command.UserToRemoveId);
        
        if (userToDelete is null)
        {
            return new Result<string>(new BadRequestError("User is not a member of this group"));
        }

        if (command.RequestingUserId != group.AdminId || userToDelete.Id != command.RequestingUserId && group.AdminId != userToDelete.Id)
        {
            return new Result<string>(new BadRequestError("You aren't a member of this group"));
        }
        
        group.RemoveMember(userToDelete);
        
        await groupsRepository.UpdateAsync(group, cancellationToken);
        await groupsRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<string>();
    }
}