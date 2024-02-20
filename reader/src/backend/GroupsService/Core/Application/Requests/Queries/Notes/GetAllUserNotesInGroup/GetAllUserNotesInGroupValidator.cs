using Application.Common;
using FluentValidation;

namespace Application.Requests.Queries.Notes.GetAllUserNotesInGroup;

public class GetAllUserNotesInGroupValidator : AbstractValidator<GetAllUserNotesInGroupQuery>
{
    public GetAllUserNotesInGroupValidator()
    {
        RuleFor(request => request.PageSettingsRequestDto)
            .PageSettings();
    }
}