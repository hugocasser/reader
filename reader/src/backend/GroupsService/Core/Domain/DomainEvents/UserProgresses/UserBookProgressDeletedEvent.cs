using Domain.Models;

namespace Domain.DomainEvents.UserProgresses;

public record UserBookProgressDeletedEvent(Guid Id) : EntityDeletedEvent<UserBookProgress>(Id);