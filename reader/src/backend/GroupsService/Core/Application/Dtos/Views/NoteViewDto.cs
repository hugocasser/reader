using Domain.Models;

namespace Application.Dtos.Views;

public record NoteViewDto(string Text, int NotePosition, string UserFirstName, string UserLastName)
{
    public static NoteViewDto MapFromModel(Note note, User user)
    {
        return new NoteViewDto(note.Text, note.NotePosition, user.FirstName, user.LastName);
    }
}