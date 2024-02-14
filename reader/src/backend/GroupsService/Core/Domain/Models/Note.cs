using Domain.Abstractions;
using Domain.DomainEvents;

namespace Domain.Models;

public class Note : Entity
{
    public int NotePosition { get; set; }
    public Guid UserBookProgressId { get; set; } = Guid.Empty;
    public UserBookProgress UserBookProgress { get; set; }
    public string Text { get; set; }

    public void CreateNote(int notePosition, UserBookProgress userBookProgress, string text)
    {
        Id = Guid.NewGuid();
        NotePosition = notePosition;
        UserBookProgressId = userBookProgress.Id;
        UserBookProgress = userBookProgress;
        Text = text;
        
        RaiseDomainEvent(EventType.Created, this);
    }
}