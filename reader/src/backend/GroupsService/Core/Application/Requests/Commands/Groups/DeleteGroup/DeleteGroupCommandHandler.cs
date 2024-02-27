using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Common;
using Application.Results;
using Application.Results.Errors;
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
            return new Result<string>(new NotFoundError("Group"));
        }

        if (command.RequestingUserId != group.AdminId)
        {
            return new Result<string>(new BadRequestError("You are not admin"));
        }
        
        group.Delete(new GroupDeletedEvent(group.Id));
        
        await groupsRepository.DeleteByIdAsync(command.GroupId, cancellationToken);
        await groupsRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<string>();
    }
}