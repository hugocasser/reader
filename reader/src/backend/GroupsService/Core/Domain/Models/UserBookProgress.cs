using System.ComponentModel.DataAnnotations;
using Domain.Abstractions;
using Domain.DomainEvents.UserProgresses;

namespace Domain.Models;

public class UserBookProgress : Entity
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public ICollection<Note> UserNotes { get; set; } = new List<Note>();
    public Guid BookId { get; set; }
    public Book? Book { get; set; }
    public Guid GroupId { get; set; }
    public Group Group { get; set; }
    [Range(0,100)]
    public int Progress { get; set; }
    public int LastReadSymbol { get; set; }

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
        RaiseDomainEvent(new UserBookProgressCreatedEvent(this));
    }

    public void UpdateUserBookProgress(int progress, int lastReadSymbol)
    {
        Progress = progress;
        LastReadSymbol = lastReadSymbol;
        RaiseDomainEvent(new UserBookProgressUpdatedEvent(this));
    }
}