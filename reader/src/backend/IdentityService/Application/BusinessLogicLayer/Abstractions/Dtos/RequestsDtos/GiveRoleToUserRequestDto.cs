using BusinessLogicLayer.Validation;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;

public class GiveRoleToUserRequestDto(Guid id, UserRole role) : BaseValidationModel<GiveRoleToUserRequestDto>
{
    
    public Guid Id { get; } = id;
    public UserRole Role { get; } = role;
}