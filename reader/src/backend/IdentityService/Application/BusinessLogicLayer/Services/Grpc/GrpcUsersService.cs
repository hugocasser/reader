using BusinessLogicLayer.Abstractions.Services.Grpc;
using BusinessLogicLayer.Common;
using BusinessLogicLayer.Options;
using DataAccessLayer.Models;
using Grpc.Net.Client;
using GrpcUsers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BusinessLogicLayer.Services.Grpc;

public class GrpcUsersService(ILogger<GrpcUsersService> _logger,
    IOptions<GrpcOptions> grpcOptions) : IGrpcUserService
{
    private readonly GrpcChannel _channel = GrpcChannel.ForAddress(grpcOptions.Value.Url);
    
    public async Task SendUserEventAsync(User user, UserEvenType userEvent)
    {
        _logger.LogInformation(" --- Starting send user event to grpc server ---");
        
        var client = new GrpcUsers.GrpcUsersService.GrpcUsersServiceClient(_channel);

        var request = new UserEventRequest()
        {
            UserId = user.Id.ToString(),
            EventType = nameof(userEvent),
            FirstName = user.FirstName,
            LastName = user.LastName,
        };

        try
        {
            var response = await client.PublishUserEventAsync(request);

            if (!response.IsSuccess)
            {
                _logger.LogInformation("Event send, but not processed");
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        
        _logger.LogInformation(" --- Finished send user event to grpc server ---");
    }
}