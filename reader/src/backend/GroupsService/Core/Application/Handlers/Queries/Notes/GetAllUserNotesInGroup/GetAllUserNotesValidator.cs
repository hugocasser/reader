using Application.Common;
using FluentValidation;

namespace Application.Handlers.Queries.Notes.GetAllUserNotes;

public class GetAllUserNotesValidator : AbstractValidator<GetAllUserNotesInGroupRequest>
{
    public GetAllUserNotesValidator()
    {
        RuleFor(request => request.PageSettingsRequestDto)
            .PageSettings();
    }
}