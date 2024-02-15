using BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;
using FluentValidation;

namespace BusinessLogicLayer.Validation.Validators;

public class GiveRoleToUserValidator : AbstractValidator<GiveRoleToUserRequestDto>
{
    public GiveRoleToUserValidator()
    {
        RuleFor(dto => dto.Id)
            .NotNull().WithMessage("Id is required");
        
        RuleFor(dto => dto.Role)
            .NotNull().WithMessage("Role is required");
        
        RuleFor(dto => dto.Role.Id)
            .NotNull().WithMessage("Id is required");
        
        RuleFor(dto => dto.Role.Name)
            .NotNull().WithMessage("Name is required");
        RuleFor(dto => dto.Role.Name)
            .NotEmpty().WithMessage("Name cannot be empty");
    }
}