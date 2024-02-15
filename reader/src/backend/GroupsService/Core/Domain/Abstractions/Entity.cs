using Domain.Abstractions.Events;

namespace Domain.Abstractions;

public abstract class Entity
{
    private readonly IList<IDomainEvent> _events = new List<IDomainEvent>();
    public Guid Id { get; protected set; } = Guid.Empty;

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _events.Add(domainEvent);
    }

    public IEnumerable<IDomainEvent> GetDomainEvents()
    {
        return _events.ToList().AsReadOnly();
    }

    public void ClearDomainEvents()
    {
        _events.Clear();
    }

    public void Delete(IDomainEvent domainEvent)
    {
        _events.Add(domainEvent);
    }
}