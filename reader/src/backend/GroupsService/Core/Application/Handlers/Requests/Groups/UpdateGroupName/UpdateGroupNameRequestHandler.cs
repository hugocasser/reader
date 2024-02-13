using Application.Abstractions.Repositories;
using Application.Common;
using Application.Dtos.Views;
using MapsterMapper;
using MediatR;

namespace Application.Handlers.Requests.Groups.UpdateGroupName;

public class UpdateGroupNameRequestHandler(IGroupsRepository groupsRepository, IMapper _mapper)
    : IRequestHandler<UpdateGroupNameRequest, Result<GroupViewDto>>
{
    public async Task<Result<GroupViewDto>> Handle(UpdateGroupNameRequest request, CancellationToken cancellationToken)
    {
        var group = await groupsRepository.GetByIdAsync(request.GroupId, cancellationToken);

        if (group is null)
        {
            return new Result<GroupViewDto>(new Error("Group not found", 404));
        }
        
        if (request.RequestingUserId != group.AdminId)
        {
            return new Result<GroupViewDto>(new Error("You are not admin of this group", 400));
        }
        
        group.UpdateGroupName(request.Name);
        
        await groupsRepository.UpdateAsync(group, cancellationToken);
        await groupsRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<GroupViewDto>(_mapper.Map<GroupViewDto>(group));
    }
}