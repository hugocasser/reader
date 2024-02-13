using Application.Common;
using FluentValidation;

namespace Application.Handlers.Queries.Notes.GetAllUserNotes;

public class GetAllUserNotesValidator : AbstractValidator<GetAllUserNotesQuery>
{
    public GetAllUserNotesValidator()
    {
        RuleFor(request => request.PageSettingsRequestDto)
            .PageSettings();
    }
}