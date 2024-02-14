using Application.Common;
using FluentValidation;

namespace Application.Handlers.Queries.Notes.GetAllGroupNotes;

public class GetAllGroupNotesValidator : AbstractValidator<GetAllGroupNotesQuery>
{
    public GetAllGroupNotesValidator()
    {
        RuleFor(request => request.PageSettingsRequestDto)
            .ReadingPageSettings();
    }
}