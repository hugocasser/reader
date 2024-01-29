using System.Text;
using BusinessLogicLayer.Abstractions.Configurations;
using BusinessLogicLayer.Abstractions.Dtos;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace BusinessLogicLayer.Services.AuthServices;

public class SendConfirmMessageEmailService(
    UserManager<User> userManager,
    IEmailSenderService emailSenderService,
    IEmailMessageSenderConfiguration configuration)
    : IEmailConfirmMessageService
{
    public async Task SendEmailConfirmMessageAsync(User user)
    {
        var token = await GenerateConfirmationToken(user);

        var confirmUrl = configuration.ConfirmUrl + $"{user.Id}/{token}";
        var emailBody = GenerateEmailBody(user, confirmUrl);
        var message = await GenerateEmailMessage(user, "Email confirmation", emailBody);
        await emailSenderService.SendEmailAsync(message);
    }

    private static string GenerateEmailBody(User user, string confirmUrl)
    {
        return $"<h1>Hi, {user.FirstName}! Thank you for registering :3</h1></br>" +
               $"Please confirm your email address <a href={System.Text.Encodings.Web.HtmlEncoder.Default.Encode(confirmUrl)}>Confirm</a>";
    }

    private Task<EmailMessage> GenerateEmailMessage(User user, string subject, string emailBody)
    {
        return Task.FromResult(new EmailMessage(user.Email, subject, emailBody, user.FirstName));
    }

    private async Task<string> GenerateConfirmationToken(User user)
    {
        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var tokenGeneratedBytes = Encoding.UTF8.GetBytes(code);
        var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);

        return codeEncoded;
    }
}