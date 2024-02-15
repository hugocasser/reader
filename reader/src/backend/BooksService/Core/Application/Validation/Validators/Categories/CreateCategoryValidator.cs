using Application.Dtos.Requests.Category;
using FluentValidation;

namespace Application.Validation.Validators.Categories;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequestDto>
{
    public CreateCategoryValidator()
    {
        RuleFor(category => category.Name)
            .NotEmpty().WithMessage("Category name can't be null");
    }
}