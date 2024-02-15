using BusinessLogicLayer.Validation;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;

public class RegisterUserRequestDto(string _email, string _password,
    string? _firstName, string? _lastName, string _username) : BaseValidationModel<RegisterUserRequestDto>
{
    public string Email { get; } = _email;
    public string Password { get; } = _password;
    public string? FirstName { get; } = _firstName;
    public string? LastName { get; } = _lastName;
    public string Username { get; init; } = _username;

    public User ToUser()
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            UserName = Username
        };
    }
}