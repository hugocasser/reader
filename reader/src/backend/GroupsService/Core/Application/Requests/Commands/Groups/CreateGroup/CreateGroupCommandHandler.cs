using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Common;
using Application.Dtos.Views;
using Application.Results;
using Application.Results.Errors;
using Domain.Models;
using MapsterMapper;
using MediatR;

namespace Application.Requests.Commands.Groups.CreateGroup;

public class CreateGroupCommandHandler(IGroupsRepository groupsRepository,
    IUsersRepository usersRepository, IMapper _mapper)
    : IRequestHandler<CreateGroupCommand, Result<GroupViewDto>>
{
    public async Task<Result<GroupViewDto>> Handle(CreateGroupCommand command, CancellationToken cancellationToken)
    {
        var admin = await usersRepository.GetByIdAsync(command.RequestingUserId ?? Guid.Empty, cancellationToken);
        
        if (admin is null)
        {
            return new Result<GroupViewDto>(new NotFoundError("User"));
        }

        var group = new Group();
        group.CreateGroup(admin, command.GroupName);
        
        await groupsRepository.CreateAsync(group, cancellationToken);
        await groupsRepository.SaveChangesAsync(cancellationToken);
        
        return new Result<GroupViewDto>(_mapper.Map<GroupViewDto>(group));
    }
}