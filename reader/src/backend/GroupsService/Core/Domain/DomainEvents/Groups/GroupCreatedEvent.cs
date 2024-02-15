using Domain.Models;

namespace Domain.DomainEvents.Groups;

public record GroupCreatedEvent(Group Entity) : EntityCreatedEvent<Group>(Entity);