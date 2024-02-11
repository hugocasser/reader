using Application.Abstractions.Repositories;
using Application.Common;
using MediatR;

namespace Application.Handlers.Requests.Groups.DeleteGroup;

public class DeleteGroupRequestHandler(IGroupsRepository groupsRepository) : IRequestHandler<DeleteGroupRequest, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteGroupRequest request, CancellationToken cancellationToken)
    {
        var group = await groupsRepository.GetByIdAsync(request.GroupId, cancellationToken);

        if (group is null)
        {
            return new Result<string>(new Error("Group not found", 404));
        }

        if (request.RequestingUserId != group.AdminId)
        {
            return new Result<string>(new Error("You are not admin of this group", 400));
        }
        
        await groupsRepository.DeleteByIdAsync(request.GroupId, cancellationToken);
        await groupsRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<string>("Group deleted");
    }
}