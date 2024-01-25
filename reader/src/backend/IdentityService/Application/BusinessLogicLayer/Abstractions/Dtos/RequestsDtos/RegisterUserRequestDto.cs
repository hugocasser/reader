namespace BusinessLogicLayer.Abstractions.Dtos;

public record RegisterUserRequestDto(string Email, string Password, string? FirstName, string? LastName) : IRequestDto;