using Domain.Abstractions.Events;
using Domain.Models;

namespace Domain.DomainEvents.UserProgresses;

public record UserBookProgressDeletedEvent(Guid Id) : IDomainEvent;