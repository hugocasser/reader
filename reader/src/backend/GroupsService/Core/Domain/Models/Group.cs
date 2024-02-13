using Domain.Abstractions;
using Domain.DomainEvents;

namespace Domain.Models;

public class Group : Entity
{
    public Guid AdminId { get; private set; }
    public ICollection<User?> Members { get; private set; } = new List<User?>();
    public ICollection<UserBookProgress> GroupProgresses { get; private set; } = new List<UserBookProgress>();
    public ICollection<Book?> AllowedBooks { get; private set; } = new List<Book?>();
    public string? GroupName { get; private set; }

    public void CreateGroup(User admin, string? groupName)
    {
        Id = Guid.NewGuid();
        AdminId = admin.Id;
        Members.Add(admin);
        GroupName = groupName;
        RaiseDomainEvent(EventType.Created, this);
    }

    private void UpdateGroup(string? groupName = null, User? newMember = null,
        User? removedMember = null, Book? newBook = null, Book? removedBook = null)
    {
        if (groupName is not null)
        {
            GroupName = groupName;
        }
        if (newMember is not null)
        {
            Members.Add(newMember);
        }
        if (removedMember is not null)
        {
            Members.Remove(removedMember);
        }
        if (newBook is not null)
        {
            AllowedBooks.Add(newBook);
        }
        if (removedBook is not null)
        {
            AllowedBooks.Remove(removedBook);
        }
        
        RaiseDomainEvent(EventType.Updated, this);
    }

    public void UpdateGroupName(string? groupName)
    {
        UpdateGroup(groupName);
    }

    public void AddMember(User? user)
    {
        UpdateGroup(newMember: user);
    }

    public void RemoveMember(User? user)
    {
        UpdateGroup(removedMember: user);
    }

    public void AddBook(Book? book)
    {
        UpdateGroup(newBook: book);
    }
    
    public void RemoveBook(Book? book)
    {
        UpdateGroup(removedBook: book);
    }
}