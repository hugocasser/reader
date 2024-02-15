using Domain.Models;

namespace Domain.DomainEvents.Users;

public record UserUpdatedEvent(User Entity) : EntityUpdatedEvent<User>(Entity);