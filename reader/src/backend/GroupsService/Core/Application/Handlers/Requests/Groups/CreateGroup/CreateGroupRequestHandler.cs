using Application.Abstractions.Repositories;
using Application.Common;
using Application.Dtos.Views;
using Domain.Models;
using MapsterMapper;
using MediatR;

namespace Application.Handlers.Requests.Groups.CreateGroup;

public class CreateGroupRequestHandler(IGroupsRepository groupsRepository,
    IUsersRepository usersRepository, IMapper _mapper)
    : IRequestHandler<CreateGroupRequest, Result<GroupViewDto>>
{
    public async Task<Result<GroupViewDto>> Handle(CreateGroupRequest request, CancellationToken cancellationToken)
    {
        var admin = await usersRepository.GetByIdAsync(request.RequestingUserId, cancellationToken);
        
        if (admin is null)
        {
            return new Result<GroupViewDto>(new Error("User not found", 404));
        }

        var group = new Group();
        group.CreateGroup(admin, request.GroupName);
        
        await groupsRepository.CreateAsync(group, cancellationToken);
        await groupsRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<GroupViewDto>(_mapper.Map<GroupViewDto>(group));
    }
}