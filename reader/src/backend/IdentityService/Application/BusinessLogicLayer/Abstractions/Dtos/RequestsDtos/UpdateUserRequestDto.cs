namespace BusinessLogicLayer.Abstractions.Dtos;

public record UpdateUserRequestDto(string OldEmail, string? NewEmail, string? FirstName, string LastName) : IRequestDto;