using FluentValidation;

namespace Application.Handlers.Queries.Notes.GetAllGroupNotes;

public class GetAllGroupNotesValidator : AbstractValidator<GetAllGroupNotesRequest>
{
    public GetAllGroupNotesValidator()
    {
        RuleFor(request => request.PageSettings)
            .NotNull().WithMessage("Page settings cannot be null");

        RuleFor(request => request.PageSettings.firstNotePosition)
            .GreaterThan(0).WithMessage("First note position must be greater than 0");
        
        RuleFor(request => request.PageSettings.lastNotePosition)
            .GreaterThan(request => request.PageSettings.firstNotePosition)
            .WithMessage("Last note position must be greater than first note position");

        RuleFor(request => request.PageSettings)
            .Custom((pageSettings, context) =>
            {
                if (pageSettings.firstNotePosition >= pageSettings.lastNotePosition)
                {
                    context.AddFailure("Page settings", "First note position must be less than last note position");
                }
            });
    }
}