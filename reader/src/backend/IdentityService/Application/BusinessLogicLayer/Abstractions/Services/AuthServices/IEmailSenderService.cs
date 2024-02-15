using BusinessLogicLayer.Abstractions.Dtos;

namespace BusinessLogicLayer.Abstractions.Services.AuthServices;

public interface IEmailSenderService
{
    public Task SendEmailAsync(EmailMessage message);
}