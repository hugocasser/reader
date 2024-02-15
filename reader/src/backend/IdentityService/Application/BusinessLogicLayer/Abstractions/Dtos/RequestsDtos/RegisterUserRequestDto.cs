using BusinessLogicLayer.Validation;

namespace BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;

public class RegisterUserRequestDto(string email, string password,
    string? firstName, string? lastName, string username) : BaseValidationModel<RegisterUserRequestDto>
{
    public string Email { get; } = email;
    public string Password { get; } = password;
    public string? FirstName { get; } = firstName;
    public string? LastName { get; } = lastName;
    public string Username { get; init; } = username;
}