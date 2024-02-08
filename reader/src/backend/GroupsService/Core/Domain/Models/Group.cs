namespace Domain.Models;

public class Group
{
    public Guid Id { get; set; }
    public Guid AdminId { get; set; }
    public ICollection<User> Members { get; set; } = new List<User>();
    
    public ICollection<Book> AllowedBooks { get; set; } = new List<Book>();
    public string GroupName { get; set; }
}