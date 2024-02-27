using Application.Abstractions.Repositories;
using Application.Common;
using Application.Dtos.Views;
using Application.Results;
using Domain.Models;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Application.Requests.Queries.Groups.GetAllGroups;

public class GetAllGroupsQueryHandler
    (IGroupsRepository _groupsRepository, 
        IMapper _mapper)
    : IRequestHandler<GetAllGroupsQuery, Result<IEnumerable<GroupViewDto>>>
{
    public async Task<Result<IEnumerable<GroupViewDto>>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
    {
        var groups = await _groupsRepository.GetAllAsync(request.PageSettingsRequestDto, cancellationToken);
        
        return new Result<IEnumerable<GroupViewDto>>(groups.Select(_mapper.Map<GroupViewDto>));
    }
}