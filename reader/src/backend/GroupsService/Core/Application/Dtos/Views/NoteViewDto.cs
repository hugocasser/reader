using Domain.Models;

namespace Application.Dtos.Views;

public record NoteViewDto(string Text, int NotePosition, string FirstName, string LastName);