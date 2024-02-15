using Application.Dtos.Requests.Authors;
using FluentValidation;

namespace Application.Validation.Validators.Authors;

public class CreateAuthorValidator : AbstractValidator<CreateAuthorRequestDto>
{
    public CreateAuthorValidator()
    {
        RuleFor(author => author.FirstName)
            .NotEmpty().WithMessage("Author first name can't be null");
        
        RuleFor(author => author.LastName)
            .NotEmpty().WithMessage("Author last name can't be null");
        
        RuleFor(author => author.Biography)
            .MaximumLength(2000).WithMessage("Author biography can't be longer than 2000 characters")
            .NotEmpty().WithMessage("Author biography can't be null");
        
        RuleFor(author => author.BirthDate)
            .NotEmpty().WithMessage("Author birth date can't be null");
        
        RuleFor(author => author.DeathDate)
            .GreaterThanOrEqualTo(author => author.BirthDate)
            .WithMessage("Author death date can't be less than birth date")
            .NotEmpty().WithMessage("Author death date can't be null");
        
        
    }
}