using Application.Common;
using FluentValidation;

namespace Application.Requests.Queries.Notes.GetAllGroupNotes;

public class GetAllGroupNotesValidator : AbstractValidator<GetAllGroupNotesQuery>
{
    public GetAllGroupNotesValidator()
    {
        RuleFor(request => request.PageSettingsRequestDto)
            .ReadingPageSettings();
    }
}