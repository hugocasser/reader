namespace BusinessLogicLayer.Abstractions.Dtos.ViewDtos;

public record AuthTokens(Guid Id, string Token, string RefreshToken);