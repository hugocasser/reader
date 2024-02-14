using Domain.Abstractions;
using Domain.DomainEvents;

namespace Domain.Models;

public class User : Entity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Group? AdminGroup { get; set; }
    public ICollection<Group> Groups { get; private set; } = new List<Group>();
    public ICollection<UserBookProgress> UserBookProgresses { get; private set; } = new List<UserBookProgress>();

    public void CreateUser(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        RaiseDomainEvent(EventType.Created, this);
    }

    public void UpdateUser(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        RaiseDomainEvent(EventType.Updated, this);
    }
}