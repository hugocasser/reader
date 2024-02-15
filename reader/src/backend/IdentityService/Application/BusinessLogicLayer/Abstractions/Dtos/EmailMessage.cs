namespace BusinessLogicLayer.Abstractions.Dtos;

public class EmailMessage(string _addressee, string _subject, string _content, string _addresseeName)
{
    public string Addressee { get; set; } = _addressee;
    public string Subject { get; set; } = _subject;
    public string Content { get; set; } = _content;
    public string AddresseeName { get; set; } = _addresseeName;
}