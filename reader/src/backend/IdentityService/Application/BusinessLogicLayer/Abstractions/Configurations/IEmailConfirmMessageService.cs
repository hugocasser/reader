using DataAccessLayer.Models;

namespace BusinessLogicLayer.Abstractions.Configurations;

public interface IEmailConfirmMessageService
{
    public Task SendEmailConfirmMessageAsync(User user);
}