using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Options;

public class EmailMessageSenderOptions
{   
    [Required(AllowEmptyStrings = false)]
    [EmailAddress]
    public string Sender { get; set; }
    [Required(AllowEmptyStrings = false)]
    [Url]
    public string SmtpServer { get; set; }
    [Required(AllowEmptyStrings = false)]
    public int Port { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string UserName { get; set; }
    [Required(AllowEmptyStrings = false)]
    [Length(8, 32)]
    public string Password { get; set; }
    [Required(AllowEmptyStrings = false)]
    [Url]
    public string ConfirmUrl { get; set; }
}