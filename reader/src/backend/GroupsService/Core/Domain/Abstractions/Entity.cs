using Domain.DomainEvents;

namespace Domain.Abstractions;

public abstract class Entity
{
    private readonly IList<IDomainEvent> _events = new List<IDomainEvent>();
    public Guid Id { get; protected set; } = Guid.Empty;

    protected void RaiseDomainEvent(EventType eventType, Entity entity)
    {
        _events.Add(new DomainEvent(eventType, entity));
    }

    public IEnumerable<IDomainEvent> GetDomainEvents()
    {
        return _events.ToList().AsReadOnly();
    }

    public void ClearDomainEvents()
    {
        _events.Clear();
    }

    public void Delete()
    {
        RaiseDomainEvent(EventType.Deleted, this);
    }
}