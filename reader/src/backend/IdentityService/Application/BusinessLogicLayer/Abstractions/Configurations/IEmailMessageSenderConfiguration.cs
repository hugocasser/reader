namespace BusinessLogicLayer.Abstractions.Configurations;

public interface IEmailMessageSenderConfiguration
{
    public string Sender { get; set; }
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    
    public string ConfirmUrl { get; set; }
}