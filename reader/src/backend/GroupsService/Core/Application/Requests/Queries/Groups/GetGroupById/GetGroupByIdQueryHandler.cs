using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Common;
using Application.Dtos.Views;
using Application.Results;
using Application.Results.Errors;
using MapsterMapper;
using MediatR;

namespace Application.Requests.Queries.Groups.GetGroupById;

public class GetGroupByIdQueryHandler(IGroupsRepository _groupsRepository, IMapper _mapper)
    : IRequestHandler<GetGroupByIdQuery, Result<GroupViewDto>>
{
    public async Task<Result<GroupViewDto>> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
    {
        var group = await _groupsRepository.GetByIdAsync(request.GroupId, cancellationToken);

        return group is null ? new Result<GroupViewDto>(new NotFoundError("Group"))
            : new Result<GroupViewDto>(_mapper.Map<GroupViewDto>(group));
    }
}