using BusinessLogicLayer.Validation;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;

public class GiveRoleToUserRequestDto(Guid _id, UserRole _role) : BaseValidationModel<GiveRoleToUserRequestDto>
{
    public Guid Id { get; } = _id;
    public UserRole Role { get; } = _role;
}