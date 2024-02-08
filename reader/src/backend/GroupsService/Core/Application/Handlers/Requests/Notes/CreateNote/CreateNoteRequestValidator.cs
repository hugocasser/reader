using FluentValidation;

namespace Application.Handlers.Requests.Notes.CreateNote;

public class CreateNoteRequestValidator : AbstractValidator<CreateNoteRequest>
{
    public CreateNoteRequestValidator()
    {
        RuleFor(note => note.Text)
            .NotEmpty().WithMessage("Text must not be empty");

        RuleFor(note => note.NotePosition)
            .NotEmpty().WithMessage("Note position must not be empty");
    }
}