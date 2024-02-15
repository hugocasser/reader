using Application.Dtos.Requests;
using FluentValidation;

namespace Application.Validation.Validators;

public class PageSettingValidator : AbstractValidator<PageSettingRequestDto>
{
    public PageSettingValidator()
    {
        RuleFor(setting => setting.PageSize)
            .NotEmpty().WithMessage("Page size can't be null")
            .GreaterThan(0).WithMessage("Page size must be greater than 0");
        
        RuleFor(settings => settings.PageNumber)
            .NotEmpty().WithMessage("Page number can't be null")
            .GreaterThan(0).WithMessage("Page number must be greater than 0");
        
    }
}