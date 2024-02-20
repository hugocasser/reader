using Application.Abstractions.Repositories;
using Application.Common;
using Application.Dtos.Views;
using MapsterMapper;
using MediatR;

namespace Application.Requests.Queries.Users.GetUserById;

public class GetUserByIdQueryHandler(IUsersRepository _usersRepository,
    IMapper _mapper) : IRequestHandler<GetUserByIdQuery, Result<UserViewDto>>
{
    public async Task<Result<UserViewDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(request.UserId, cancellationToken);
        
        return user is null ? new Result<UserViewDto>(new Error("User not found", 404)) 
            : new Result<UserViewDto>(_mapper.Map<UserViewDto>(user));
    }
}