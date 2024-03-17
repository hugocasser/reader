using Application.Dtos.Requests;
using Application.Dtos.Views;
using FluentValidation;

namespace Application.Common;

public static class CustomValidationRules
{
    public static IRuleBuilderOptions<T, PageSettingsRequestDto> PageSettings<T>
        (this IRuleBuilder<T, PageSettingsRequestDto> ruleBuilder)
    {
        return ruleBuilder.Must(pageSettings => pageSettings.Page > 0 && pageSettings.PageSize > 0)
            .WithMessage("Page settings properties must be greater than 0");
    }

    public static IRuleBuilderOptions<T, ReadingPageSettingsRequestDto> ReadingPageSettings<T>
        (this IRuleBuilder<T, ReadingPageSettingsRequestDto> ruleBuilder)
    {
        return ruleBuilder.Must(pageSettings => pageSettings.LastNotePosition > 0 && pageSettings.FirstNotePosition > 0)
            .WithMessage("Page settings properties must be greater than 0")
            .Must(pageSettings => pageSettings.LastNotePosition > pageSettings.FirstNotePosition)
            .WithMessage("Last note position must be greater than first note position");
    }
}