using BusinessLogicLayer.Validation;

namespace BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;

public class LoginUserRequestDto(string email, string password) : BaseValidationModel<LoginUserRequestDto>
{
    public string Email { get; } = email;
    public string Password { get; } = password;
}