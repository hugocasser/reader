using Domain.Abstractions.Events;
using Domain.Models;

namespace Domain.DomainEvents.Groups;

public record GroupDeletedEvent(Guid Id) : IDomainEvent;