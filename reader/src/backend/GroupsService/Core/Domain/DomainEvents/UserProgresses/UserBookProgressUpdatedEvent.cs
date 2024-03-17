using Domain.Abstractions.Events;
using Domain.Models;

namespace Domain.DomainEvents.UserProgresses;

public record UserBookProgressUpdatedEvent(UserBookProgress Entity) : IDomainEvent;