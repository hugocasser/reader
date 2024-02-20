using Domain.Abstractions.Events;
using Domain.Models;

namespace Domain.DomainEvents.Users;

public record UserDeletedEvent(Guid Id) : IDomainEvent;