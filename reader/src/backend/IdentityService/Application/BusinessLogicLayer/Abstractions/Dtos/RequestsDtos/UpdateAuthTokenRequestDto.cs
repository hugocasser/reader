using BusinessLogicLayer.Validation;

namespace BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;

public class UpdateAuthTokenRequestDto(Guid userId, string refreshToken)
    : BaseValidationModel<UpdateAuthTokenRequestDto>
{
    public Guid UserId { get; init; } = userId;
    public string RefreshToken { get; init; } = refreshToken;
}