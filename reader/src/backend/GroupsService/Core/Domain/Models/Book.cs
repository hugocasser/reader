using Domain.Abstractions;
using Domain.DomainEvents;
using Domain.DomainEvents.Books;

namespace Domain.Models;

public class Book : Entity
{
    public string BookName { get; set; }
    public string AuthorFirstName { get; set; }
    public string AuthorLastName { get; set; }
    public ICollection<UserBookProgress> UserBookProgress { get; set; } = new List<UserBookProgress>();
    public ICollection<Group> AllowedInGroups { get; set; } = new List<Group>();
    
    public void CreateBook(string bookName, string authorFirstName, string authorLastName, string text, Guid id)
    {
        BookName = bookName;
        AuthorFirstName = authorFirstName;
        AuthorLastName = authorLastName;
        Id = id;
        
        RaiseDomainEvent(new BookCreatedEvent(this));
    }

    public void UpdateBook(string bookName, string authorFirstName, string authorLastName)
    {
        BookName = bookName;
        AuthorFirstName = authorFirstName;
        AuthorLastName = authorLastName;
        RaiseDomainEvent(new BookUpdatedEvent(this));
    }
}