using Domain.Abstractions.Events;
using Domain.Models;

namespace Domain.DomainEvents.Books;

public record BookCreatedEvent(Book Entity) : IDomainEvent;