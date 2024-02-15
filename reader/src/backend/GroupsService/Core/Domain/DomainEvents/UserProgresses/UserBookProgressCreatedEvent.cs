using Domain.Models;

namespace Domain.DomainEvents.UserProgresses;

public record UserBookProgressCreatedEvent(UserBookProgress Entity) : EntityCreatedEvent<UserBookProgress>(Entity);