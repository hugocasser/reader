namespace Application.Dtos.Views;

public record UserViewDto
    (string FirstName,
        string LastName,
        Guid Id,
        GroupViewDto Group = null);