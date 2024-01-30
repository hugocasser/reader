using Application.Dtos.Requests.Books;
using FluentValidation;

namespace Application.Validation.Validator.Books;

public class CreateBookValidator : AbstractValidator<CreateBookRequest>
{
    public CreateBookValidator()
    {
        RuleFor(book => book.Name)
            .NotNull().WithMessage("Book name can't be null");
        RuleFor(book => book.Description)
            .MaximumLength(2000).WithMessage("Book description can't be longer than 2000 characters")
            .NotNull().WithMessage("Book description can't be null");
        RuleFor(book => book.AuthorId)
            .NotNull().WithMessage("Author id can't be null");
        RuleFor(book => book.CategoryId)
            .NotNull().WithMessage("Category id can't be null");
        RuleFor(book => book.Text)
            .NotNull().WithMessage("Book text can't be null");
    }
}