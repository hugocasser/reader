using BusinessLogicLayer.Validation;

namespace BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;

public class LoginUserRequestDto(string _email, string _password) : BaseValidationModel<LoginUserRequestDto>
{
    public string Email { get; } = _email;
    public string Password { get; } = _password;
}