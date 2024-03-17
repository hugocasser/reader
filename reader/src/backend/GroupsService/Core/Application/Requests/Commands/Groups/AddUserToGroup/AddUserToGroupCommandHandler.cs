using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Common;
using Application.Results;
using Application.Results.Errors;
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
            return new Result<string>(new NotFoundError("User"));
        }
        
        var group = await groupsRepository.GetByIdAsync(command.GroupId, cancellationToken);

        if (group is null)
        {
            return new Result<string>(new NotFoundError("Group"));
        }

        if (command.RequestingUserId!= group.Members.FirstOrDefault(user => user.Id == command.RequestingUserId)?.Id)
        {
            return new Result<string>(new BadRequestError("You not a member of this group"));
        }

        if (group.Members.Any(user => user.Id == command.InvitedUser))
        {
            return new Result<string>(new BadRequestError("User is already a member of this group"));
        }
        
        group.AddMember(invitedUser);
        
        await groupsRepository.UpdateAsync(group, cancellationToken);
        await groupsRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<string>();
    }
}