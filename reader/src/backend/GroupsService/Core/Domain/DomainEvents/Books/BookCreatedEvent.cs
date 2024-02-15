using Domain.Models;

namespace Domain.DomainEvents.Books;

public record BookCreatedEvent(Book Entity) : EntityCreatedEvent<Book>(Entity);