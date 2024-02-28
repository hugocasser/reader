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
        _logger.LogInformation("--> Grpc request started: CreateUser()...");
            
        var user = new User
        {
            Id = Guid.Parse(request.UserId)
        };
        user.CreateUser(request.FirstName, request.LastName);

        await _usersRepository.CreateAsync(user, context.CancellationToken);
        await _usersRepository.SaveChangesAsync(context.CancellationToken);
        
        _logger.LogInformation("--> Grpc request finished: CreateUser()...");
        
        return  new CreateUserResponse
        {
            Success = true
        };
    }

    public override async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
    {
        _logger.LogInformation("--> Grpc request started: UpdateUser()...");
        var user = new User
        {
            Id = Guid.Parse(request.UserId)
        };
        user.UpdateUser(request.FirstName, request.LastName);
        
        await _usersRepository.UpdateAsync(user, context.CancellationToken);
        await _usersRepository.SaveChangesAsync(context.CancellationToken);
        
        _logger.LogInformation("--> Grpc request finished: UpdateUser()...");
        
        return new UpdateUserResponse
        {
            Success = true
        };
    }

    public override async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
    {
        _logger.LogInformation("--> Grpc request started: DeleteUser()...");
        
        await _usersRepository.DeleteByIdAsync(Guid.Parse(request.UserId), context.CancellationToken);
        await _usersRepository.SaveChangesAsync(context.CancellationToken);
        
        _logger.LogInformation("--> Grpc request finished: DeleteUser()...");
        
        return new DeleteUserResponse
        {
            Success = true
        };
    } 
}