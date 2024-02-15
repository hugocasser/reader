using Domain.Models;

namespace Domain.DomainEvents.UserProgresses;

public record UserBookProgressUpdatedEvent(UserBookProgress Entity) : EntityUpdatedEvent<UserBookProgress>(Entity);