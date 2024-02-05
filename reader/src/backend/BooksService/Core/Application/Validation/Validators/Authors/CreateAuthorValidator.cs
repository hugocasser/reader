using Application.Dtos.Requests.Authors;
using FluentValidation;

namespace Application.Validation.Validators.Authors;

public class CreateAuthorValidator : AbstractValidator<CreateAuthorRequest>
{
    public CreateAuthorValidator()
    {
        RuleFor(author => author.FirstName)
            .NotNull().WithMessage("Author first name can't be null");
        
        RuleFor(author => author.LastName)
            .NotNull().WithMessage("Author last name can't be null");
        
        RuleFor(author => author.Biography)
            .MaximumLength(2000).WithMessage("Author biography can't be longer than 2000 characters")
            .NotNull().WithMessage("Author biography can't be null");
        
        RuleFor(author => author.BirthDate)
            .NotNull().WithMessage("Author birth date can't be null");
        
        RuleFor(author => author.DeathDate)
            .GreaterThanOrEqualTo(author => author.BirthDate)
            .WithMessage("Author death date can't be less than birth date")
            .NotNull().WithMessage("Author death date can't be null");
        
        
    }
}