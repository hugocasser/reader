using Application.Dtos.Requests.Books;
using FluentValidation;

namespace Application.Validation.Validators.Books;

public class UpdateBookInfoValidator : AbstractValidator<UpdateBookInfoRequest>
{
    public UpdateBookInfoValidator()
    {
        RuleFor(book => book.Id)
            .NotNull().WithMessage("Book id can't be null");
        
        RuleFor(book => book.Name)
            .NotNull().WithMessage("Book name can't be null");
        
        RuleFor(book => book.Description)
            .MaximumLength(2000).WithMessage("Book description can't be longer than 2000 characters")
            .NotNull().WithMessage("Book description can't be null");
        
        RuleFor(book => book.AuthorId)
            .NotNull().WithMessage("Author id can't be null");
        
        RuleFor(book => book.CategoryId)
            .NotNull().WithMessage("Category id can't be null");
    }
}