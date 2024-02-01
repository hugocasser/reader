using Application.Dtos.Requests;
using FluentValidation;

namespace Application.Validation.Validators;

public class PageSettingValidator : AbstractValidator<PageSetting>
{
    public PageSettingValidator()
    {
        RuleFor(setting => setting.PageSize)
            .NotNull().WithMessage("Page size can't be null")
            .GreaterThan(0).WithMessage("Page size must be greater than 0");
        RuleFor(settings => settings.PageNumber)
            .NotNull().WithMessage("Page number can't be null")
            .GreaterThan(0).WithMessage("Page number must be greater than 0");
        RuleFor(setting => setting.DescriptionMaxLength)
            .NotNull().WithMessage("Description max length can't be null");
        RuleFor(setting => setting.ShowDescription)
            .NotNull().WithMessage("Show description can't be null");
        
    }
}