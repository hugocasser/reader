using Domain.Abstractions.Events;
using Domain.Models;

namespace Domain.DomainEvents.Books;

public record BookUpdatedEvent(Book Entity) : IDomainEvent;