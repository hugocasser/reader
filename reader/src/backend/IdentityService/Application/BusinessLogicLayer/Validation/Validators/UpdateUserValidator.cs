using BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;
using FluentValidation;

namespace BusinessLogicLayer.Validation.Validators;

public class UpdateUserValidator : AbstractValidator<UpdateUserRequestDto>
{
    public UpdateUserValidator()
    {
        RuleFor(dto => dto.OldEmail)
            .NotNull().WithMessage("Old email is required");
        
        RuleFor(dto => dto.OldEmail)
            .EmailAddress().WithMessage("Old email is not valid");
        
        RuleFor(dto => dto.NewEmail)
            .EmailAddress().WithMessage(" New email is not valid");
        
        RuleFor(dto => dto.FirstName)
            .NotNull().WithMessage("First name is required");
        
        RuleFor(dto => dto.LastName)
            .NotNull().WithMessage("Last name is required");
    }
}