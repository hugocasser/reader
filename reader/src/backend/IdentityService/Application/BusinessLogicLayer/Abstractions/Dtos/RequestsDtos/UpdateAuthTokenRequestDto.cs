namespace BusinessLogicLayer.Abstractions.Dtos;

public record UpdateAuthTokenRequestDto(Guid UserId, string RefreshToken);