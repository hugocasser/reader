using Domain.Models;

namespace Domain.DomainEvents.Books;

public record BookUpdatedEvent(Book Entity) : EntityUpdatedEvent<Book>(Entity);