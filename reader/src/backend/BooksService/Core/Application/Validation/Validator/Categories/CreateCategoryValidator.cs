using Application.Dtos.Requests.Category;
using FluentValidation;

namespace Application.Validation.Validator.Categories;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryValidator()
    {
        RuleFor(category => category.Name)
            .NotNull().WithMessage("Category name can't be null");
    }
}