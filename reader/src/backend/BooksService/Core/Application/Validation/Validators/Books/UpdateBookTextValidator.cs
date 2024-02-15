using Application.Dtos.Requests.Books;
using FluentValidation;

namespace Application.Validation.Validators.Books;

public class UpdateBookTextValidator : AbstractValidator<UpdateBookTextRequestDto>
{
    public UpdateBookTextValidator()
    {
        RuleFor(book => book.Id)
            .NotEmpty().WithMessage("Book id can't be null");
        
        RuleFor(book => book.Text)
            .NotEmpty().WithMessage("Book text can't be null");
    }
}