using Application.Abstractions.Repositories;
using Domain.Models;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class GrpcUsersService(ILogger<GrpcUsersService> _logger, IUsersRepository _usersRepository)
    : UsersService.UsersServiceBase
{
    public override async Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        var user = new User
        {
            Id = Guid.Parse(request.UserId)
        };
        user.CreateUser(request.FirstName, request.LastName);

        await _usersRepository.CreateAsync(user, context.CancellationToken);
        await _usersRepository.SaveChangesAsync(context.CancellationToken);
        
        return  new CreateUserResponse
        {
            UserId = user.Id.ToString()
        };
        
    }

    public override async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
    {
        var user = new User
        {
            Id = Guid.Parse(request.UserId)
        };
        user.UpdateUser(request.FirstName, request.LastName);
        
        await _usersRepository.UpdateAsync(user, context.CancellationToken);
        await _usersRepository.SaveChangesAsync(context.CancellationToken);
        
        return new UpdateUserResponse
        {
            UserId = user.Id.ToString()
        };
    }

    public override async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
    {
        await _usersRepository.DeleteByIdAsync(Guid.Parse(request.UserId), context.CancellationToken);
        await _usersRepository.SaveChangesAsync(context.CancellationToken);
        return new DeleteUserResponse
        {
            UserId = request.UserId
        };
    } 
}