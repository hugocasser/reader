using Application.Abstractions.Repositories;
using Application.Common;
using Domain.DomainEvents.Groups;
using MediatR;

namespace Application.Requests.Commands.Groups.DeleteGroup;

public class DeleteGroupCommandHandler(IGroupsRepository groupsRepository)
    : IRequestHandler<DeleteGroupCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteGroupCommand command, CancellationToken cancellationToken)
    {
        var group = await groupsRepository.GetByIdAsync(command.GroupId, cancellationToken);

        if (group is null)
        {
            return new Result<string>(new Error("Group not found", 404));
        }

        if (command.RequestingUserId != group.AdminId)
        {
            return new Result<string>(new Error("You are not admin of this group", 400));
        }
        
        group.Delete(new GroupDeletedEvent(group.Id));
        
        await groupsRepository.DeleteByIdAsync(command.GroupId, cancellationToken);
        await groupsRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<string>("Group deleted");
    }
}