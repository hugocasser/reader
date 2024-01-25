namespace BusinessLogicLayer.Abstractions.Dtos;

public record AuthTokens(Guid Id, string Token, string RefreshToken);