using BusinessLogicLayer.Abstractions.Services.Grpc;
using BusinessLogicLayer.Options;
using DataAccessLayer.Models;
using Grpc.Net.Client;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BusinessLogicLayer.Services.Grpc;

public class GrpcUsersService(ILogger<GrpcUsersService> _logger,
    IOptions<GrpcOptions> _grpcOptions, IMapper _mapper) : IGrpcUserService
{
    private readonly GrpcChannel _channel = GrpcChannel.ForAddress(_grpcOptions.Value.Url);


    public async Task SendUserCreatedAsync(User user)
    {
        var client = new GrpcUsers.GrpcUsersClient(_channel);
        var request = _mapper.Map<CreateUserRequest>(user);

        try
        { 
            await client.CreateUserAsync(request);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
        
    }

    public async Task SendUserUpdatedAsync(User user)
    {
        var client = new GrpcUsers.GrpcUsersClient(_channel);
        var request = _mapper.Map<UpdateUserRequest>(user);

        try
        {
            await client.UpdateUserAsync(request);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public async Task SendUserDeletedAsync(User user)
    {
        var client = new GrpcUsers.GrpcUsersClient(_channel);
        var request = _mapper.Map<DeleteUserRequest>(user);

        try
        {
            await client.DeleteUserAsync(request);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}