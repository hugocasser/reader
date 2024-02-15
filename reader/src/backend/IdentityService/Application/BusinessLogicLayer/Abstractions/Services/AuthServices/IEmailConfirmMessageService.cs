using DataAccessLayer.Models;

namespace BusinessLogicLayer.Abstractions.Services.AuthServices;

public interface IEmailConfirmMessageService
{
    public Task SendEmailConfirmMessageAsync(User user);
}