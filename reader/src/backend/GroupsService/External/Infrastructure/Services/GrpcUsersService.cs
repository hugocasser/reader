using Application.Abstractions.Repositories;
using Domain.DomainEvents.Users;
using Domain.Models;
using Grpc.Core;
using GrpcUsers;
using Infrastructure.Common;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class GrpcUsersService(ILogger<GrpcUsersService> _logger, IUsersRepository _usersRepository, IMapper _mapper)
    : GrpcUsers.GrpcUsersService.GrpcUsersServiceBase
{
    public override async Task<GrpcUsersResponse> PublishUserEvent(UserEventRequest request, ServerCallContext context)
    {
        _logger.LogInformation(" --- PublishUserEvent request started ---");

        switch (request.EventType)
        {
            case nameof(UserEventType.Created):
            {
                var guidId = Guid.Parse(request.UserId);
                var isUserExist = await _usersRepository.IsExistByIdAsync(guidId, context.CancellationToken);

                if (isUserExist)
                {
                    _logger.LogError("User already exist");

                    return new GrpcUsersResponse()
                    {
                        IsSuccess = false
                    };
                }

                var user = new User()
                {
                    Id = guidId,
                };

                user.CreateUser(request.FirstName, request.LastName);
                await _usersRepository.CreateAsync(user, context.CancellationToken);

                break;
            }
            case nameof(UserEventType.Updated):
            {
                var guidId = Guid.Parse(request.UserId);
                var user = await _usersRepository.GetByIdAsync(guidId, context.CancellationToken);

                if (user is null)
                {
                    return new GrpcUsersResponse()
                    {
                        IsSuccess = false
                    };
                }
                
                user.UpdateUser(request.FirstName, request.LastName);
                await _usersRepository.UpdateAsync(user, context.CancellationToken);
                
                break;
            }
            case nameof(UserEventType.Deleted):
            {
                var guidId = Guid.Parse(request.UserId);
                var user = await _usersRepository.GetByIdAsync(guidId, context.CancellationToken);

                if (user is null)
                {
                    _logger.LogError("User not found");

                    return new GrpcUsersResponse
                    {
                        IsSuccess = false
                    };
                }

                user.Delete(new UserDeletedEvent(user.Id));
                await _usersRepository.DeleteByIdAsync(guidId, context.CancellationToken);

                break;
            }
        }

        await _usersRepository.SaveChangesAsync(context.CancellationToken);

        _logger.LogInformation(" --- PublishUserEvent request ended ---");

        return new GrpcUsersResponse()
        {
            IsSuccess = true
        };
    }
}