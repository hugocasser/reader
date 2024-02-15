using Domain.Models;

namespace Domain.DomainEvents.Users;

public record UserCreatedEvent(User Entity) : EntityCreatedEvent<User>(Entity);