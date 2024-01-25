using DataAccessLayer.Models;

namespace BusinessLogicLayer.Abstractions.Dtos;

public record ViewUserDto(string Email, string Role, string FirstName, string LastName)
{
    public static ViewUserDto MapFromModel(User user, string role)
    {
        return new ViewUserDto(user.Email, role, user.FirstName, user.LastName);
    }   
}