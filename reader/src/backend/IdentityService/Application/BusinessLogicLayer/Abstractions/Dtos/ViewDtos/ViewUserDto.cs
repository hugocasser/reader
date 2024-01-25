using DataAccessLayer.Models;

namespace BusinessLogicLayer.Abstractions.Dtos;

public record ViewUserDto(string Email, IEnumerable<string> Roles, string FirstName, string LastName)
{
    public static ViewUserDto MapFromModel(User user, IEnumerable<string> roles)
    {
        return new ViewUserDto(user.Email, roles, user.FirstName, user.LastName);
    }   
}