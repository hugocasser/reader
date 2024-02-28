using DataAccessLayer.Models;

namespace BusinessLogicLayer.Abstractions.Services.Grpc;

public interface IGrpcUserService
{
    public Task SendUserCreatedAsync(User user);
    public Task SendUserUpdatedAsync(User user);
    public Task SendUserDeletedAsync(User user);
}