using BusinessLogicLayer.Abstractions.Configurations;
using BusinessLogicLayer.Abstractions.Dtos;
using MailKit.Net.Smtp;
using MimeKit;

namespace BusinessLogicLayer.Services.AuthServices;

public class EmailSenderService(IEmailMessageSenderConfiguration configuration) : IEmailSenderService
{
    public async Task SendEmailAsync(EmailMessage message)
    {
        await SendAsync(CreateEmailMessage(message));
    }
    
    private MimeMessage CreateEmailMessage(EmailMessage message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("Identity", configuration.Sender));
        emailMessage.To.Add(new MailboxAddress(message.AddresseeName, message.Addressee));
        emailMessage.Subject = message.Subject;

        var bodyBuilder = new BodyBuilder 
        { 
            HtmlBody = message.Content
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
                configuration.SmtpServer,
                configuration.Port,
                true);
            client.AuthenticationMechanisms
                .Remove("XOAUTH2");
            await client.AuthenticateAsync(
                configuration.UserName,
                configuration.Password);

            await client.SendAsync(mailMessage);
        }
        catch(Exception exception)
        {
            throw new Exception(exception.Message);
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
    }
}