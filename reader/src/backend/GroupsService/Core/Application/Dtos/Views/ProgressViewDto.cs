namespace Application.Dtos.Views;

public record ProgressViewDto(Guid Id, GroupViewDto Group, UserViewDto User, string BookName, int Progress, int NotesCount);