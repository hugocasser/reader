using Domain.Abstractions;

namespace Domain.Models;

public class Book : IEntity
{
    public Guid Id { get; set; }
    public string BookName { get; set; }
    public string AuthorFirstName { get; set; }
    public string AuthorLastName { get; set; }
    public ICollection<UserBookProgress> UserBookProgress { get; set; } = new List<UserBookProgress>();
    public ICollection<Group> AllowedInGroups { get; set; } = new List<Group>();
}