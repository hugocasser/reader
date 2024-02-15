using DataAccessLayer.Models;

namespace BusinessLogicLayer.Abstractions.Dtos.ViewDtos;

public record ViewUserDto(Guid Id, string Email, IEnumerable<string> Roles, string FirstName, string LastName)
{
    public static ViewUserDto MapFromModel(User user, IEnumerable<string> roles)
    {
        return new ViewUserDto(user.Id,user.Email, roles, user.FirstName, user.LastName);
    }   
}