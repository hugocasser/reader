using BusinessLogicLayer.Validation;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;

public class UpdateUserRequestDto(string oldEmail, string? newEmail, string? firstName,
    string lastName) : BaseValidationModel<UpdateAuthTokenRequestDto>
{
    public string OldEmail { get; init; } = oldEmail;
    public string? NewEmail { get; init; } = newEmail;
    public string? FirstName { get; init; } = firstName;
    public string LastName { get; init; } = lastName;

    public void UpdateUser(User user)
    {
        user.FirstName = FirstName;
        user.LastName = LastName;
        user.Email = NewEmail;
    }
}