using System.ComponentModel.DataAnnotations.Schema;

namespace CartIQ.Domain.Common;

public class BaseEntity
{
    public Guid Id { get; set; }

    // Domain event reactions happen only on transaction commits
    private readonly List<BaseEvent> _domainEvents = new();

    [NotMapped]
    public IReadOnlyList<BaseEvent> DomainEvents =>  _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }
    
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}