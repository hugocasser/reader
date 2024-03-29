using BusinessLogicLayer.Abstractions.Dtos.RequestsDtos;
using FluentValidation;

namespace BusinessLogicLayer.Validation.Validators;

public class LoginValidator : AbstractValidator<LoginUserRequestDto>
{
    public LoginValidator()
    {
        RuleFor(dto => dto.Email)
            .NotNull().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid");
        
        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password cannot be empty")
            .MinimumLength(8).WithMessage("Password length must be at least 8.")
            .MaximumLength(64).WithMessage("Password length must not exceed 32.")
            .Matches(@"[A-Z]+").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Password must contain at least one number.")
            .Matches(@"[\!\?\*\.\(\)\-\$\%\&\@]+")
                .WithMessage("Password must contain at least one (!? *. () - $ % & @).");
    }
}