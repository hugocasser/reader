namespace Domain.Models;

public class Note
{
    public Guid Id { get; set; }
    public int NotePosition { get; set; }
    public Guid UserBookProgressId { get; set; }
    public UserBookProgress UserBookProgress { get; set; }
    public string Text { get; set; }
}