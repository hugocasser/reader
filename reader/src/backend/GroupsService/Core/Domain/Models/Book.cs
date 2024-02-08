namespace Domain.Models;

public class Book
{
    public Guid Id { get; set; }
    public string BookName { get; set; }
    public string AuthorFirstName { get; set; }
    public string AuthorLastName { get; set; }
    public ICollection<UserBookProgress> UserBookProgress { get; set; } = new List<UserBookProgress>();
}