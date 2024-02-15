using Domain.Models;

namespace Domain.DomainEvents.Users;

public record UserDeletedEvent(Guid Id) : EntityDeletedEvent<User>(Id);