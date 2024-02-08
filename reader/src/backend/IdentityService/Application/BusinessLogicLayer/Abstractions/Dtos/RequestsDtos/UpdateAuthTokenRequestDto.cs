using BusinessLogicLayer.Validation;

namespace BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;

public class UpdateAuthTokenRequestDto(Guid _userId, string _refreshToken)
    : BaseValidationModel<UpdateAuthTokenRequestDto>
{
    public Guid UserId { get; init; } = _userId;
    public string RefreshToken { get; init; } = _refreshToken;
}