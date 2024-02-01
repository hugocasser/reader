using BusinessLogicLayer.Abstractions.Dtos;
using BusinessLogicLayer.Abstractions.Services.AuthServices;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BusinessLogicLayer.Services.AuthServices;

public class EmailSenderService(IOptions<EmailMessageSenderOptions> options) : IEmailSenderService
{
    private readonly EmailMessageSenderOptions _options = options.Value;
    
    public async Task SendEmailAsync(EmailMessage message)
    {
        await SendAsync(CreateEmailMessage(message));
    }
    
    private MimeMessage CreateEmailMessage(EmailMessage message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("Identity", _options.Sender));
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
                _options.SmtpServer,
                _options.Port,
                true);
            client.AuthenticationMechanisms
                .Remove("XOAUTH2");
            await client.AuthenticateAsync(
                _options.UserName,
                _options.Password);

            await client.SendAsync(mailMessage);
        }
        catch(Exception ex)
        {
            throw new EmailNotSentException(ex.Message);
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
    }
}