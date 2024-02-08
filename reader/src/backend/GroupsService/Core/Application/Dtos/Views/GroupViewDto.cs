namespace Application.Dtos.Views;

public record GroupViewDto(UserViewDto Admin, ICollection<UserViewDto> MembersDtos,
    string GroupName, ICollection<BookViewDto> BookViewDtos,
    ICollection<NoteViewDto> NoteViewDtos);