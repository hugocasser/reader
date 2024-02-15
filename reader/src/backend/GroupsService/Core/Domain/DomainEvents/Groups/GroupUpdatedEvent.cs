using Domain.Models;

namespace Domain.DomainEvents.Groups;

public record GroupUpdatedEvent(Group Group) : EntityUpdatedEvent<Group>(Group);