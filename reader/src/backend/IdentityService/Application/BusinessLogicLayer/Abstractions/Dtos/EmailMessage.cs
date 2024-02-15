using Microsoft.AspNetCore.Http;
using MimeKit;

namespace BusinessLogicLayer.Abstractions.Dtos;

public class EmailMessage(string addressee, string subject, string content, string addresseeName)
{
    public string Addressee { get; set; } = addressee;
    public string Subject { get; set; } = subject;
    public string Content { get; set; } = content;
    
    public string AddresseeName { get; set; } = addresseeName;
}