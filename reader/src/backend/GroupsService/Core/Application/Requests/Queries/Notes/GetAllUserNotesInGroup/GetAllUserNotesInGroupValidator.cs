using Application.Common;
using Application.Requests.Queries.Notes.GetAllUserNotesInGroup;
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