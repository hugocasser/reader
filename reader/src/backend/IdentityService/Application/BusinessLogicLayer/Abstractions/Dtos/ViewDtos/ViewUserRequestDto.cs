using DataAccessLayer.Models;

namespace BusinessLogicLayer.Abstractions.Dtos;

public record ViewUserRequestDto(string Email, string Role, string FirstName, string LastName) : IRequestDto
{
    public static ViewUserRequestDto MapFromModel(User user, string role)
    {
        return new ViewUserRequestDto(user.Email, role, user.FirstName, user.LastName);
    }   
}