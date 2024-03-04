using BusinessLogicLayer.Common;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Abstractions.Services.Grpc;

public interface IGrpcUserService
{
    public Task SendUserEventAsync(User user, UserEvenType userEvent);
}