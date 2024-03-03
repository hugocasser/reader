using Domain.Abstractions;
using Domain.Models;

namespace Domain.Messages;

public record BookCreatedMessage(Book Book) : IMessage
{
    public Guid Id { get; init; } =  Book.Id;
    public string BookName { get; init; } = Book.Name;
    public string? AuthorFirstName { get; init; } = Book.AuthorFirstName;
    public string? AuthorLastName { get; init; } = Book.AuthorLastName;
}