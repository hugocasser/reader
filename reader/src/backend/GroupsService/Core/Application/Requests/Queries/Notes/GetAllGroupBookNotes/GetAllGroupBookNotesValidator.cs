using Application.Common;
using FluentValidation;

namespace Application.Requests.Queries.Notes.GetAllGroupBookNotes;

public class GetAllGroupBookNotesValidator : AbstractValidator<GetAllGroupBookNotesQuery>
{
    public GetAllGroupBookNotesValidator()
    {
        RuleFor(request =>
            request.PageSettingsRequestDto).ReadingPageSettings();
    }
}