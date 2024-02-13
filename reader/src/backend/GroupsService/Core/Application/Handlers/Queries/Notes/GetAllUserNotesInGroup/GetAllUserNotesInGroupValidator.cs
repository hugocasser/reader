using Application.Common;
using FluentValidation;

namespace Application.Handlers.Queries.Notes.GetAllUserNotes;

public class GetAllUserNotesInGroupValidator : AbstractValidator<GetAllUserNotesInGroupQuery>
{
    public GetAllUserNotesInGroupValidator()
    {
        RuleFor(request => request.PageSettingsRequestDto)
            .PageSettings();
    }
}