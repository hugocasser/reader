using Domain.Abstractions;
using Domain.Models;

namespace Domain.Messages;

public class BookUpdatedMessage(Book Book) : IMessage
{
    public Guid Id { get; set; } = Book.Id;
    public string? BookName { get; set; } = Book.Name;
    public string? AuthorFirstName { get; set; } = Book.AuthorFirstName;
    public string? AuthorLastName { get; set; } = Book.AuthorLastName;
}