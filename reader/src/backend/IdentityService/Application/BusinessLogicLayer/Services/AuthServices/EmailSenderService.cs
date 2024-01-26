using BusinessLogicLayer.Abstractions.Configurations;
using BusinessLogicLayer.Abstractions.Dtos;
using BusinessLogicLayer.Abstractions.Services;
using BusinessLogicLayer.Abstractions.Services.AuthServices;
using MailKit.Net.Smtp;
using MimeKit;

namespace BusinessLogicLayer.Services.AuthServices;

public class EmailSenderService : IEmailSenderService
{
    private readonly IEmailMessageSenderConfiguration _configuration;

    public EmailSenderService(IEmailMessageSenderConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task SendEmailAsync(EmailMessage message)
    {
        await SendAsync(CreateEmailMessage(message));
    }
    
    private MimeMessage CreateEmailMessage(EmailMessage message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("Identity", _configuration.Sender));
        emailMessage.To.Add(new MailboxAddress(message.AddresseeName, message.Addressee));
        emailMessage.Subject = message.Subject;

        var bodyBuilder = new BodyBuilder 
        { 
            HtmlBody = $"<h2 style='color:red;'>{message.Content}</h2>" 
        };

        emailMessage.Body = bodyBuilder.ToMessageBody();
        return emailMessage;
    }
    
    private async Task SendAsync(MimeMessage mailMessage)
    {
        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(
                _configuration.SmtpServer,
                _configuration.Port,
                true);
            client.AuthenticationMechanisms
                .Remove("XOAUTH2");
            await client.AuthenticateAsync(
                _configuration.UserName,
                _configuration.Password);

            await client.SendAsync(mailMessage);
        }
        catch
        {
            // TODO: throw an exception
            throw;
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
    }
}