namespace Application.Dtos.Views;

public record NoteViewDto(Guid Id, string Text, int NotePosition, string FirstName, string LastName);