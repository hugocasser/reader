using Domain.Abstractions;
using Domain.DomainEvents;
using Domain.DomainEvents.Users;

namespace Domain.Models;

public class User : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Group? AdminGroup { get; set; }
    public ICollection<Group> Groups { get; private set; } = new List<Group>();
    public ICollection<UserBookProgress> UserBookProgresses { get; private set; } = new List<UserBookProgress>();

    public void CreateUser(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        RaiseDomainEvent(new UserCreatedEvent(this));
    }

    public void UpdateUser(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        RaiseDomainEvent(new UserUpdatedEvent(this));
    }
}