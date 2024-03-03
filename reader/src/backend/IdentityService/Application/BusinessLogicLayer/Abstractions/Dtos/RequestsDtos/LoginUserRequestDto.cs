using BusinessLogicLayer.Validation;

namespace BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;

public class LoginUserRequestDto: BaseValidationModel<LoginUserRequestDto>
{
    public string Email { get; set; } 
    public string Password { get; set; } 
}