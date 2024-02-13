using System.ComponentModel.DataAnnotations;
using Domain.Abstractions;
using Domain.DomainEvents;

namespace Domain.Models;

public class UserBookProgress : Entity
{
    public Guid UserId { get; private set; }
    public User? User { get; private set; }
    public ICollection<Note> UserNotes { get; private set; } = new List<Note>();
    public Guid BookId { get; private set; }
    public Book? Book { get; private set; }
    public Guid GroupId { get; private set; }
    public Group Group { get; private set; }
    [Range(0,100)]
    public int Progress { get; private set; }
    public int LastReadSymbol { get; private set; }

    public void CreateUserBookProgress(Book book, Group group, User user)
    {
        User = user;
        UserId = user.Id;
        Group = group;
        GroupId = group.Id;
        Book = book;
        BookId = book.Id;
        Progress = 0;
        LastReadSymbol = 0;
        RaiseDomainEvent(EventType.Created, this);
    }

    public void UpdateUserBookProgress(int progress, int lastReadSymbol)
    {
        Progress = progress;
        LastReadSymbol = lastReadSymbol;
        RaiseDomainEvent(EventType.Updated, this);
    }
}