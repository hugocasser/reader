namespace BusinessLogicLayer.Abstractions.Dtos;

public record LoginUserRequestDto(string Email, string Password) : IRequestDto;