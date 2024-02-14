using Application.Abstractions.Repositories;
using Application.Common;
using Application.Dtos.Views;
using MapsterMapper;
using MediatR;

namespace Application.Requests.Commands.Groups.UpdateGroupName;

public class UpdateGroupNameCommandHandler(IGroupsRepository groupsRepository, IMapper _mapper)
    : IRequestHandler<UpdateGroupNameCommand, Result<GroupViewDto>>
{
    public async Task<Result<GroupViewDto>> Handle(UpdateGroupNameCommand command, CancellationToken cancellationToken)
    {
        var group = await groupsRepository.GetByIdAsync(command.GroupId, cancellationToken);

        if (group is null)
        {
            return new Result<GroupViewDto>(new Error("Group not found", 404));
        }
        
        if (command.RequestingUserId != group.AdminId)
        {
            return new Result<GroupViewDto>(new Error("You are not admin of this group", 400));
        }
        
        group.UpdateGroupName(command.Name);
        
        await groupsRepository.UpdateAsync(group, cancellationToken);
        await groupsRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<GroupViewDto>(_mapper.Map<GroupViewDto>(group));
    }
}