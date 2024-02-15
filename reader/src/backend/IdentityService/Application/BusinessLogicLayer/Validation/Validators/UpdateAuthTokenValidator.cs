using BusinessLogicLayer.Abstractions.Dtos;
using BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;
using FluentValidation;

namespace BusinessLogicLayer.Validation.Validators;

public class UpdateAuthTokenValidator : AbstractValidator<UpdateAuthTokenRequestDto>
{
    public UpdateAuthTokenValidator()
    {
        RuleFor(dto => dto.RefreshToken)
            .NotNull().WithMessage("RefreshToken is required");
    }
    
}