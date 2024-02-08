using BusinessLogicLayer.Validation;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;

public class UpdateUserRequestDto(string _oldEmail, string? _newEmail, string? _firstName,
    string _lastName) : BaseValidationModel<UpdateAuthTokenRequestDto>
{
    public string OldEmail { get; init; } = _oldEmail;
    public string? NewEmail { get; init; } = _newEmail;
    public string? FirstName { get; init; } = _firstName;
    public string LastName { get; init; } = _lastName;

    public void UpdateUser(User user)
    {
        user.FirstName = FirstName;
        user.LastName = LastName;
        user.Email = NewEmail;
    }
}