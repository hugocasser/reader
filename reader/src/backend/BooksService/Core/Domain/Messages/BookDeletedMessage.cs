using Domain.Abstractions;
using Domain.Models;

namespace Domain.Messages;

public record BookDeletedMessage(Book Book) : IMessage
{
    public Guid Id { get; init; } =  Book.Id;
};