using FluentValidation;

namespace Application.Requests.Commands.Notes.CreateNote;

public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
{
    public CreateNoteCommandValidator()
    {
        RuleFor(note => note.Text)
            .NotEmpty().WithMessage("Text must not be empty");

        RuleFor(note => note.NotePosition)
            .NotEmpty().WithMessage("Note position must not be empty");
    }
}