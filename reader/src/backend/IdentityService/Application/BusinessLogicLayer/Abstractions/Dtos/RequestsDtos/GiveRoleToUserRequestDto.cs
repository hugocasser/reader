using DataAccessLayer.Models;

namespace BusinessLogicLayer.Abstractions.Dtos;

public record GiveRoleToUserRequestDto(Guid id, UserRole role) : IRequestDto;