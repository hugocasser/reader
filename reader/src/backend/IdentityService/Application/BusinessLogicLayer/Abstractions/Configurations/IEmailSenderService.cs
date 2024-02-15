using BusinessLogicLayer.Abstractions.Dtos;

namespace BusinessLogicLayer.Abstractions.Configurations;

public interface IEmailSenderService
{
    public Task SendEmailAsync(EmailMessage message);
}