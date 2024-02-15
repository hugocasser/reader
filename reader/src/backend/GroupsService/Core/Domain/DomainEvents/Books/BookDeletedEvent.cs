using Domain.Models;

namespace Domain.DomainEvents.Books;

public record BookDeletedEvent(Guid Id) : EntityDeletedEvent<Book>(Id);