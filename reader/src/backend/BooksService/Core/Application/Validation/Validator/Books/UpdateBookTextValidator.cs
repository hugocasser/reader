using Application.Dtos.Requests.Books;
using FluentValidation;

namespace Application.Validation.Validator.Books;

public class UpdateBookTextValidator : AbstractValidator<UpdateBookTextRequest>
{
    public UpdateBookTextValidator()
    {
        RuleFor(book => book.Id)
            .NotNull().WithMessage("Book id can't be null");
        RuleFor(book => book.Text)
            .NotNull().WithMessage("Book text can't be null");
    }
}