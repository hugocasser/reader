using Domain.Abstractions;
using Domain.DomainEvents;

namespace Domain.Models;

public class Book : Entity
{
    public string BookName { get; private set; }
    public string AuthorFirstName { get; private set; }
    public string AuthorLastName { get; private set; }
    public ICollection<UserBookProgress> UserBookProgress { get; set; } = new List<UserBookProgress>();
    public ICollection<Group> AllowedInGroups { get; set; } = new List<Group>();
    
    public void CreateBook(string bookName, string authorFirstName, string authorLastName, string text, Guid id)
    {
        BookName = bookName;
        AuthorFirstName = authorFirstName;
        AuthorLastName = authorLastName;
        Id = id;
        
        RaiseDomainEvent(EventType.Created, this);
    }

    public void UpdateBook(string bookName, string authorFirstName, string authorLastName)
    {
        BookName = bookName;
        AuthorFirstName = authorFirstName;
        AuthorLastName = authorLastName;
        RaiseDomainEvent(EventType.Updated, this);
    }
}