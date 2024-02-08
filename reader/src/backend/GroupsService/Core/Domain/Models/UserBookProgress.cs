using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class UserBookProgress
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public ICollection<Note> UserNotes { get; set; } = new List<Note>();
    public Guid BookId { get; set; }
    public Book Book { get; set; }
    public Guid GroupId { get; set; }
    public Group Group { get; set; }
    [Range(0,100)]
    public int Progress { get; set; }
    public int LastReadSymbol { get; set; }
}