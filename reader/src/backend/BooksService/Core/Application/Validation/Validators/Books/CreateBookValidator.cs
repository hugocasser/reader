using Application.Dtos.Requests.Books;
using FluentValidation;

namespace Application.Validation.Validators.Books;

public class CreateBookValidator : AbstractValidator<CreateBookRequestDto>
{
    public CreateBookValidator()
    {
        RuleFor(book => book.Name)
            .NotEmpty().WithMessage("Book name can't be null");
        
        RuleFor(book => book.Description)
            .MaximumLength(2000).WithMessage("Book description can't be longer than 2000 characters")
            .NotEmpty().WithMessage("Book description can't be null");
        
        RuleFor(book => book.AuthorId)
            .NotEmpty().WithMessage("Author id can't be null");
        
        RuleFor(book => book.CategoryId)
            .NotEmpty().WithMessage("Category id can't be null");
        
        RuleFor(book => book.Text)
            .NotEmpty().WithMessage("Book text can't be null");
    }
}