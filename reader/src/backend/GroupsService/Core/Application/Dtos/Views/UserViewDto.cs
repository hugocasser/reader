using Domain.Models;

namespace Application.Dtos.Views;

public record UserViewDto(string FirstName, string LastName)
{
    public static UserViewDto MapFromModel(User user)
    {
        return new UserViewDto(user.FirstName, user.LastName);
    }
};