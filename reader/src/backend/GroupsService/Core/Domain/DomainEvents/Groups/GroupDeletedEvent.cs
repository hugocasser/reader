using Domain.Models;

namespace Domain.DomainEvents.Groups;

public record GroupDeletedEvent(Guid Id) : EntityDeletedEvent<Group>(Id);