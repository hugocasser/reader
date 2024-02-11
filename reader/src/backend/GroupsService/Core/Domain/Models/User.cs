using Domain.Abstractions;

namespace Domain.Models;

public class User : IEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public ICollection<Group> Groups { get; set; } = new List<Group>();
    public ICollection<UserBookProgress> UserBookProgresses { get; set; } = new List<UserBookProgress>();
}